using Falcon.Protocol.Frame;
using Falcon.Protocol.Handshake;
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

        int bufferSize = 8192;

        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        public WebSocketServer()
        {
            server = new ServerListener(bufferSize);
            webSocketClientsManager = new WebSocketClientsManager();
            handshakeResponseGenerator = new HandshakeResponseGenerator();
            framesManager = new FramesManager();

            server.WebSocketConnected += OnWebSocketConnected;
            server.WebSocketDataReceived += OnWebSocketDataReceived;
            server.WebSocketDataSent += OnWebSocketDataSent;
            server.WebSocketDisconnected += OnWebSocketDisconnected;
        }

        public void Open(IPAddress address, int port)
        {
            var endPoint = new IPEndPoint(address, port);
            server.StartListening(endPoint);
        }

        public void Close()
        {
            server.StopListening();
        }

        public bool SendData(String clientID, byte[] data)
        {
            if (!webSocketClientsManager.Exists(clientID))
                return false;

            var frameBytes = framesManager.Serialize(data);
            server.SendData(clientID, frameBytes);
            return true;
        }

        void SendRawData(String clientID, byte[] data)
        {
            server.SendData(clientID, data);
        }

        void OnWebSocketConnected(object sender, WebSocketConnectedEventArgs args)
        {
            var webSocketClient = new WebSocketClient(args.ClientID, bufferSize);
            webSocketClientsManager.Add(webSocketClient);

            WebSocketConnected(this, args);
        }

        void OnWebSocketDataReceived(object sender, WebSocketDataReceivedEventArgs args)
        {
            var client = webSocketClientsManager.Get(args.ClientID);
            client.AddToBuffer(args.Data);

            if (!client.HandshakeDone)
                DoHandshake(client);
            else
                ProcessMessage(client);
        }

        void OnWebSocketDataSent(object sender, WebSocketDataSentEventArgs args)
        {
            WebSocketDataSent(this, args);
        }

        void OnWebSocketDisconnected(object sender, WebSocketDisconnectedEventArgs args)
        {
            var webSocketClient = webSocketClientsManager.Get(args.ClientID);
            webSocketClientsManager.Remove(webSocketClient);

            WebSocketDisconnected(this, args);
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

            if(parsedBytes > 0)
            {
                if (frameType == FrameType.Message)
                    WebSocketDataReceived(this, new WebSocketDataReceivedEventArgs(client.ID, message));
                else if (frameType == FrameType.Disconnect)
                    server.CloseClientConnection(client.ID);

                client.RemoveFromBuffer(parsedBytes);
            }
        }
    }
}
