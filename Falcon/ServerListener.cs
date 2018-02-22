using Falcon.SocketServices;
using Falcon.SocketServices.EventArguments;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Falcon
{
    internal class ServerListener : IServerListener, IDisposable
    {
        Socket _socket;
        Task _loop;

        ConnectingService _newConnectionService;
        ReceivingDataService _receiveDataService;
        SendingDataService _sendDataService;

        uint _bufferSize;
        static ManualResetEvent _loopEvent;

        public event EventHandler<ConnectedEventArgs> ClientConnected;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<DataSentEventArgs> DataSent;
        public event EventHandler<DisconnectedEventArgs> ClientDisconnected;
        
        public EServerState ServerState { get; private set; }

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

        public void Start(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _loop = new Task(Loop);

            ServerState = EServerState.Working;

            _socket.Bind(endpoint);
            _socket.Listen(128);

            _loop.Start();
        }

        public void Stop()
        {
            ServerState = EServerState.Closed;

            _loopEvent.Set();
            _socket.Close();
        }

        public void Send(Socket socket, byte[] data)
        {
            _sendDataService.SendData(socket, data);
        }

        public void CloseConnection(Socket socket)
        {
            CloseConnection(socket, null);
        }

        public void CloseConnection(Socket socket, Exception ex)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            OnDisconnected(this, new DisconnectedEventArgs(socket, ex));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _loop?.Dispose();
                _socket?.Dispose();
            }
        }

        void Loop()
        {
            while (ServerState == EServerState.Working)
            {
                _loopEvent.Reset();
                _newConnectionService.BeginConnection(_socket);
                _loopEvent.WaitOne();
            }
        }

        void OnConnected(object sender, ConnectedEventArgs e)
        {
            ClientConnected(this, new ConnectedEventArgs(e.Socket));

            _receiveDataService.ReceiveData(e.Socket);
            _loopEvent.Set();
        }

        void OnReceivedData(object sender, DataReceivedEventArgs e)
        {
            DataReceived(this, e);
            _receiveDataService.ReceiveData(e.Socket);
        }

        void OnSentData(object sender, DataSentEventArgs e)
        {
            DataSent(this, e);
        }

        void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            ClientDisconnected(this, e);
        }
    }
}
