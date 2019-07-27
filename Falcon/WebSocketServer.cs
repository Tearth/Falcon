using System;
using System.Net;
using System.Text;
using Falcon.CommandExecutors;
using Falcon.Exceptions;
using Falcon.Protocol.Frame;
using Falcon.Protocol.Handshake;
using Falcon.SocketServices.EventArguments;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;

namespace Falcon
{
    /// <summary>
    /// Main class of the library. Represents a set of methods to manage WebSocket server.
    /// </summary>
    public class WebSocketServer : IWebSocketServer, IDisposable
    {
        private readonly IServerListener _server;
        private readonly IWebSocketClientsManager _webSocketClientsManager;
        private readonly IHandshakeResponseGenerator _handshakeResponseGenerator;
        private readonly IFramesManager _framesManager;
        private readonly ICommandExecutorFactory _commandsExecutorFactory;

        /// <summary>
        /// Buffer size for each client. Default is 8192
        /// </summary>
        public uint BufferSize { get; }

        /// <summary>
        /// Current state of server
        /// </summary>
        public EServerState ServerState => _server.ServerState;

        /// <inheritdoc />
        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;

        /// <inheritdoc />
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;

        /// <inheritdoc />
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;

        /// <inheritdoc />
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketServer"/> class.
        /// </summary>
        public WebSocketServer() : this(8192, new ServerListener(8192))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketServer"/> class.
        /// </summary>
        /// <param name="bufferSize">The buffer size for each client.</param>
        public WebSocketServer(uint bufferSize) : this(bufferSize, new ServerListener(bufferSize))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketServer"/> class.
        /// </summary>
        /// <param name="bufferSize">The buffer size for each client.</param>
        /// <param name="serverListener">The server listener custom implementation.</param>
        public WebSocketServer(uint bufferSize, IServerListener serverListener)
        {
            BufferSize = bufferSize;

            _server = serverListener;
            _webSocketClientsManager = new WebSocketClientsManager();
            _handshakeResponseGenerator = new HandshakeResponseGenerator();
            _framesManager = new FramesManager();
            _commandsExecutorFactory = new CommandExecutorFactory();

            _server.ClientConnected += OnConnected;
            _server.DataReceived += OnDataReceived;
            _server.DataSent += OnDataSent;
            _server.ClientDisconnected += OnDisconnected;
        }

        /// <inheritdoc />
        public void Start(IPAddress address, ushort port)
        {
            if (ServerState != EServerState.Closed)
            {
                throw new ServerAlreadyWorkingException();
            }

            var endpoint = new IPEndPoint(address, port);
            _server.Start(endpoint);
        }

        /// <inheritdoc />
        public void Stop()
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyClosedException();
            }

            _server.Stop();
        }

        /// <inheritdoc />
        public bool SendData(string clientId, byte[] data)
        {
            return SendData(clientId, data, FrameType.Message);
        }

        /// <inheritdoc />
        public bool SendData(string clientId, string text)
        {
            return SendData(clientId, Encoding.UTF8.GetBytes(text), FrameType.Message);
        }

        /// <inheritdoc />
        public bool SendData(string clientId, byte[] data, FrameType type)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyClosedException();
            }

            var webSocketClient = _webSocketClientsManager.GetById(clientId);
            if (webSocketClient == null)
            {
                return false;
            }

            var frameBytes = _framesManager.Serialize(data, type);
            _server.Send(webSocketClient.Socket, frameBytes);

            return true;
        }

        /// <inheritdoc />
        public bool SendRawData(string clientId, byte[] data)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyClosedException();
            }

            var webSocketClient = _webSocketClientsManager.GetById(clientId);
            if (webSocketClient == null)
            {
                return false;
            }

            _server.Send(webSocketClient.Socket, data);

            return true;
        }

        /// <inheritdoc />
        public ClientInfo GetClientInfo(string clientId)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyWorkingException();
            }

            var webSocketClient = _webSocketClientsManager.GetById(clientId);
            return webSocketClient?.GetInfo();
        }

        /// <inheritdoc />
        public void DisconnectClient(string clientId)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyClosedException();
            }

            var webSocketClient = _webSocketClientsManager.GetById(clientId);
            if (webSocketClient == null)
            {
                return;
            }

            _server.CloseConnection(webSocketClient.Socket);
            _webSocketClientsManager.Remove(webSocketClient);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ((IDisposable)_server)?.Dispose();

            GC.SuppressFinalize(this);
        }

        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            var webSocketClient = new WebSocketClient(e.Socket, BufferSize);
            _webSocketClientsManager.Add(webSocketClient);

            WebSocketConnected?.Invoke(this, new WebSocketConnectedEventArgs(webSocketClient.Id));
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var webSocketClient = _webSocketClientsManager.GetBySocket(e.Socket);
            if (webSocketClient == null)
            {
                return;
            }

            if (!webSocketClient.Buffer.Add(e.Data))
            {
                _server.CloseConnection(webSocketClient.Socket, new BufferOverflowException());
                return;
            }

            if (!webSocketClient.HandshakeDone)
            {
                DoHandshake(webSocketClient);
            }
            else
            {
                ProcessMessage(webSocketClient);
            }
        }

        private void OnDataSent(object sender, DataSentEventArgs e)
        {
            var webSocketClient = _webSocketClientsManager.GetBySocket(e.Socket);
            if (webSocketClient == null)
            {
                return;
            }

            WebSocketDataSent?.Invoke(this, new WebSocketDataSentEventArgs(webSocketClient.Id, e.BytesSent));
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            var webSocketClient = _webSocketClientsManager.GetBySocket(e.Socket);
            if (webSocketClient == null)
            {
                return;
            }

            _webSocketClientsManager.Remove(webSocketClient);

            WebSocketDisconnected?.Invoke(this, new WebSocketDisconnectedEventArgs(webSocketClient.Id, e.Exception));
        }

        private void DoHandshake(WebSocketClient client)
        {
            var response = _handshakeResponseGenerator.GetResponse(client.Buffer.GetData());
            if (response.Length > 0)
            {
                SendRawData(client.Id, response);

                client.HandshakeDone = true;
                client.Buffer.Clear();
            }
        }

        private void ProcessMessage(WebSocketClient client)
        {
            var frame = client.Buffer.GetData();
            var message = _framesManager.Deserialize(frame, out _, out var frameType, out var parsedBytes);

            if (parsedBytes > 0)
            {
                var commandExecutor = _commandsExecutorFactory.Create(frameType);

                if (commandExecutor.Do(this, client.Id, message))
                {
                    WebSocketDataReceived?.Invoke(this, new WebSocketDataReceivedEventArgs(client.Id, message));
                }

                client.Buffer.Remove(parsedBytes);
            }
        }
    }
}
