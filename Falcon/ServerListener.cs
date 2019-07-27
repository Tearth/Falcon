using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Falcon.SocketServices;
using Falcon.SocketServices.EventArguments;

namespace Falcon
{
    /// <summary>
    /// Represents a set of methods to manage server socket.
    /// </summary>
    public class ServerListener : IServerListener, IDisposable
    {
        private Socket _socket;
        private Task _loop;

        private readonly ConnectingService _newConnectionService;
        private readonly ReceivingDataService _receiveDataService;
        private readonly SendingDataService _sendDataService;

        private readonly uint _bufferSize;
        private static ManualResetEvent _loopEvent;

        /// <inheritdoc />
        public event EventHandler<ConnectedEventArgs> ClientConnected;

        /// <inheritdoc />
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        /// <inheritdoc />
        public event EventHandler<DataSentEventArgs> DataSent;

        /// <inheritdoc />
        public event EventHandler<DisconnectedEventArgs> ClientDisconnected;

        /// <inheritdoc />
        public EServerState ServerState { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerListener"/> class.
        /// </summary>
        /// <param name="bufferSize">The buffer size for each client.</param>
        public ServerListener(uint bufferSize)
        {
            _bufferSize = bufferSize;
            _loopEvent = new ManualResetEvent(false);

            _newConnectionService = new ConnectingService();
            _receiveDataService = new ReceivingDataService();
            _sendDataService = new SendingDataService();

            _newConnectionService.Connected += OnConnected;
            _newConnectionService.Disconnected += OnDisconnected;

            _receiveDataService.ReceivedData += OnReceivedData;
            _receiveDataService.Disconnected += OnDisconnected;

            _sendDataService.SentData += OnSentData;
            _sendDataService.Disconnected += OnDisconnected;

            ServerState = EServerState.Closed;
        }

        /// <inheritdoc />
        public void Start(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _loop = new Task(Loop);

            ServerState = EServerState.Working;

            _socket.Bind(endpoint);
            _socket.Listen(128);

            _loop.Start();
        }

        /// <inheritdoc />
        public void Stop()
        {
            ServerState = EServerState.Closed;

            _loopEvent.Set();
            _socket.Close();
        }

        /// <inheritdoc />
        public void Send(Socket socket, byte[] data)
        {
            _sendDataService.SendData(socket, data);
        }

        /// <inheritdoc />
        public void CloseConnection(Socket socket)
        {
            CloseConnection(socket, null);
        }

        /// <inheritdoc />
        public void CloseConnection(Socket socket, Exception ex)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            OnDisconnected(this, new DisconnectedEventArgs(socket, ex));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _loop?.Dispose();
            _socket?.Dispose();

            GC.SuppressFinalize(this);
        }

        private void Loop()
        {
            while (ServerState == EServerState.Working)
            {
                _loopEvent.Reset();
                _newConnectionService.BeginConnection(_socket);
                _loopEvent.WaitOne();
            }
        }

        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            ClientConnected?.Invoke(this, new ConnectedEventArgs(e.Socket));

            _receiveDataService.ReceiveData(e.Socket, _bufferSize);
            _loopEvent.Set();
        }

        private void OnReceivedData(object sender, DataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, e);
            _receiveDataService.ReceiveData(e.Socket, _bufferSize);
        }

        private void OnSentData(object sender, DataSentEventArgs e)
        {
            DataSent?.Invoke(this, e);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            ClientDisconnected?.Invoke(this, e);
        }
    }
}
