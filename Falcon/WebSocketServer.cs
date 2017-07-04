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

        public WebSocketServer()
        {
            server = new ServerListener();

            server.OnWebSocketNewConnection += OnWebSocketNewConnection;
            server.OnWebSocketDataReceived += OnWebSocketDataReceived;
            server.OnWebSocketDataSent += OnWebSocketDataSent;
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

        void OnWebSocketNewConnection(object sender, WebSocketNewConnectionArgs args)
        {
            throw new NotImplementedException();
        }

        void OnWebSocketDataReceived(object sender, WebSocketReceivedDataArgs args)
        {
            throw new NotImplementedException();
        }

        void OnWebSocketDataSent(object sender, WebSocketSentDataArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
