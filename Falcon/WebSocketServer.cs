using Falcon.CommandsExecutors;
using Falcon.Exceptions;
using Falcon.Protocol.Frame;
using Falcon.Protocol.Handshake;
using Falcon.SocketServices.ClientInformations;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;
using System;
using System.Net;

namespace Falcon
{
    public class WebSocketServer
    {
        ServerListener server;
        WebSocketClientsManager webSocketClientsManager;
        HandshakeResponseGenerator handshakeResponseGenerator;
        FramesManager framesManager;
        CommandExecutorFactory commandsExecutorFactory;

        /// <summary>
        /// Buffer size for each client. Default is 8192
        /// </summary>
        public int BufferSize { get; private set; }

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

        public WebSocketServer() : this(8192)
        {

        }

        public WebSocketServer(int bufferSize)
        {
            this.BufferSize = bufferSize;

            this.server = new ServerListener(BufferSize);
            this.webSocketClientsManager = new WebSocketClientsManager();
            this.handshakeResponseGenerator = new HandshakeResponseGenerator();
            this.framesManager = new FramesManager();
            this.commandsExecutorFactory = new CommandExecutorFactory();

            this.server.WebSocketConnected += OnWebSocketConnected;
            this.server.WebSocketDataReceived += OnWebSocketDataReceived;
            this.server.WebSocketDataSent += OnWebSocketDataSent;
            this.server.WebSocketDisconnected += OnWebSocketDisconnected;
        }

        /// <summary>
        /// Opens WebSocket server with specified address and port.
        /// </summary>
        public void Open(IPAddress address, int port)
        {
            var endPoint = new IPEndPoint(address, port);
            server.StartListening(endPoint);
        }
        
        /// <summary>
        /// Closes WebSocket server
        /// </summary>
        public void Close()
        {
            server.StopListening();
        }

        /// <summary>
        /// Sends data (as WebSocket frame) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public bool SendData(string clientID, byte[] data)
        {
            return SendData(clientID, data, FrameType.Message);
        }

        public bool SendData(string clientID, byte[] data, FrameType type)
        {
            if (!webSocketClientsManager.Exists(clientID))
                return false;

            var frameBytes = framesManager.Serialize(data, type);
            server.SendData(clientID, frameBytes);
            return true;
        }

        /// <summary>
        /// Sends raw data (without creating WebSocket frame) to connected client with the specified id.
        /// Returns false if clientID not exists.
        /// </summary>
        public void SendRawData(string clientID, byte[] data)
        {
            server.SendData(clientID, data);
        }

        /// <summary>
        /// Returns informations about client with specified id. If not exists, returns null.
        /// </summary>
        public ClientInfo GetClientInfo(string clientID)
        {
            return server.GetClientInfo(clientID);
        }

        /// <summary>
        /// Disconnects client with specified id.
        /// </summary>
        public void DisconnectClient(string clientID)
        {
            server.CloseConnection(clientID);
        }

        void OnWebSocketConnected(object sender, WebSocketConnectedEventArgs args)
        {
            var webSocketClient = new WebSocketClient(args.ClientID, BufferSize);
            webSocketClientsManager.Add(webSocketClient);

            WebSocketConnected?.Invoke(this, args);
        }

        void OnWebSocketDataReceived(object sender, WebSocketDataReceivedEventArgs args)
        {
            var client = webSocketClientsManager.Get(args.ClientID);

            if(!client.AddToBuffer(args.Data))
            {
                server.CloseConnection(args.ClientID, new BufferOverflowException());
                return;
            }

            if (!client.HandshakeDone)
                DoHandshake(client);
            else
                ProcessMessage(client);
        }

        void OnWebSocketDataSent(object sender, WebSocketDataSentEventArgs args)
        {
            WebSocketDataSent?.Invoke(this, args);
        }

        void OnWebSocketDisconnected(object sender, WebSocketDisconnectedEventArgs args)
        {
            var webSocketClient = webSocketClientsManager.Get(args.ClientID);
            webSocketClientsManager.Remove(webSocketClient);

            WebSocketDisconnected?.Invoke(this, args);
        }

        void DoHandshake(WebSocketClient client)
        {
            var response = handshakeResponseGenerator.GetResponse(client.GetBufferData());
            if (response != null)
            {
                SendRawData(client.ID, response);

                client.HandshakeDone = true;
                client.ClearBuffer();
            }
        }

        void ProcessMessage(WebSocketClient client)
        {
            var frame = client.GetBufferData();

            var decryptResult = DecryptResult.None;
            var frameType = FrameType.None;
            var parsedBytes = 0;
            var message = framesManager.Deserialize(frame, out decryptResult, out frameType, out parsedBytes);

            if (parsedBytes > 0)
            {
                var commandExecutor = commandsExecutorFactory.Create(frameType);

                if (commandExecutor.Do(this, client.ID, message))
                    WebSocketDataReceived?.Invoke(this, new WebSocketDataReceivedEventArgs(client.ID, message));

                client.RemoveFromBuffer(parsedBytes);
            }
        }
    }
}
