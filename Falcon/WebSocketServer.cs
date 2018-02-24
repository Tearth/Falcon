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
    public class WebSocketServer : IWebSocketServer, IDisposable
    {
        IServerListener _server;
        IWebSocketClientsManager _webSocketClientsManager;
        IHandshakeResponseGenerator _handshakeResponseGenerator;
        IFramesManager _framesManager;
        ICommandExecutorFactory _commandsExecutorFactory;

        /// <summary>
        /// Buffer size for each client. Default is 8192
        /// </summary>
        public uint BufferSize { get; private set; }

        /// <summary>
        /// Current state of server
        /// </summary>
        public EServerState ServerState => _server.ServerState;

        /// <summary>
        /// Event triggered when a new client has connected to the server
        /// </summary>
        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;

        /// <summary>
        /// Event triggered when a new data has received from connected client
        /// </summary>
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;

        /// <summary>
        /// Event triggered when data has been succesfully sent
        /// </summary>
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;

        /// <summary>
        /// Event triggered when the client has disconnected from the server
        /// </summary>
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        public WebSocketServer() : this(8192, new ServerListener(8192))
        {

        }

        public WebSocketServer(uint bufferSize) : this(bufferSize, new ServerListener(bufferSize))
        {

        }

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

        /// <summary>
        /// Opens WebSocket server with specified address and port.
        /// </summary>
        public void Start(IPAddress address, ushort port)
        {
            if (ServerState != EServerState.Closed)
            {
                throw new ServerAlreadyWorkingException();
            }

            var endpoint = new IPEndPoint(address, port);
            _server.Start(endpoint);
        }

        /// <summary>
        /// Closes WebSocket server
        /// </summary>
        public void Stop()
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyClosedException();
            }

            _server.Stop();
        }

        /// <summary>
        /// Sends data (as WebSocket frame) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public bool SendData(string clientID, byte[] data)
        {
            return SendData(clientID, data, FrameType.Message);
        }

        /// <summary>
        /// Sends text (as WebSocket frame) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public bool SendData(string clientID, string text)
        {
            return SendData(clientID, Encoding.UTF8.GetBytes(text), FrameType.Message);
        }

        /// <summary>
        /// Sends data (as WebSocket frame with specified type) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public bool SendData(string clientID, byte[] data, FrameType type)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyWorkingException();
            }

            var webSocketClient = _webSocketClientsManager.GetByID(clientID);
            if (webSocketClient == null)
            {
                return false;
            }

            var frameBytes = _framesManager.Serialize(data, type);
            _server.Send(webSocketClient.Socket, frameBytes);

            return true;
        }

        /// <summary>
        /// Sends raw data (without creating WebSocket frame) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public void SendRawData(string clientID, byte[] data)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyWorkingException();
            }

            var webSocketClient = _webSocketClientsManager.GetByID(clientID);
            if (webSocketClient == null)
            {
                return;
            }

            _server.Send(webSocketClient.Socket, data);
        }

        /// <summary>
        /// Returns information about client with specified id. If not exists, returns null.
        /// </summary>
        public ClientInfo GetClientInfo(string clientID)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyWorkingException();
            }

            var webSocketClient = _webSocketClientsManager.GetByID(clientID);
            return webSocketClient?.GetInfo();
        }

        /// <summary>
        /// Disconnects client with specified id.
        /// </summary>
        public void DisconnectClient(string clientID)
        {
            if (ServerState != EServerState.Working)
            {
                throw new ServerAlreadyWorkingException();
            }

            var webSocketClient = _webSocketClientsManager.GetByID(clientID);
            if (webSocketClient == null)
            {
                return;
            }

            _server.CloseConnection(webSocketClient.Socket);
            _webSocketClientsManager.Remove(webSocketClient);
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
                ((IDisposable)_server)?.Dispose();
            }
        }

        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            var webSocketClient = new WebSocketClient(e.Socket, BufferSize);
            _webSocketClientsManager.Add(webSocketClient);

            WebSocketConnected?.Invoke(this, new WebSocketConnectedEventArgs(webSocketClient.ID));
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

            WebSocketDataSent?.Invoke(this, new WebSocketDataSentEventArgs(webSocketClient.ID, e.BytesSent));
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            var webSocketClient = _webSocketClientsManager.GetBySocket(e.Socket);
            if (webSocketClient == null)
            {
                return;
            }

            _webSocketClientsManager.Remove(webSocketClient);

            WebSocketDisconnected?.Invoke(this, new WebSocketDisconnectedEventArgs(webSocketClient.ID, e.Exception));
        }

        private void DoHandshake(WebSocketClient client)
        {
            var response = _handshakeResponseGenerator.GetResponse(client.Buffer.GetData());
            if (response.Length > 0)
            {
                SendRawData(client.ID, response);

                client.HandshakeDone = true;
                client.Buffer.Clear();
            }
        }

        private void ProcessMessage(WebSocketClient client)
        {
            var frame = client.Buffer.GetData();

            var deserializeResult = DeserializeResult.None;
            var message = _framesManager.Deserialize(frame, out deserializeResult, out var frameType, out var parsedBytes);

            if (parsedBytes > 0)
            {
                var commandExecutor = _commandsExecutorFactory.Create(frameType);

                if (commandExecutor.Do(this, client.ID, message))
                {
                    WebSocketDataReceived?.Invoke(this, new WebSocketDataReceivedEventArgs(client.ID, message));
                }

                client.Buffer.Remove(parsedBytes);
            }
        }
    }
}
