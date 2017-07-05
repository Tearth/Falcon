using Falcon.Protocol.Handshake;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Falcon
{
    public class WebSocketServer
    {
        ServerListener server;
        WebSocketClientsManager webSocketClientsManager;
        HandshakeResponseGenerator handshakeResponseGenerator;

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

        void OnWebSocketConnected(object sender, WebSocketConnectedEventArgs args)
        {
            var webSocketClient = new WebSocketClient(args.ClientID, bufferSize);
            webSocketClientsManager.Add(webSocketClient);

            WebSocketConnected(this, args);
        }

        void OnWebSocketDataReceived(object sender, WebSocketDataReceivedEventArgs args)
        {
            
        }

        void OnWebSocketDataSent(object sender, WebSocketDataSentEventArgs args)
        {
            
        }

        void OnWebSocketDisconnected(object sender, WebSocketDisconnectedEventArgs args)
        {
            var webSocketClient = webSocketClientsManager.GetByID(args.ClientID);
            webSocketClientsManager.Remove(webSocketClient);

            WebSocketDisconnected(this, args);
        }
    }
}
