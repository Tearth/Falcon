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
            server.OnWebSocketNewData += OnWebSocketNewData;
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

        void OnWebSocketNewConnection(object sender, string clientID)
        {
            throw new NotImplementedException();
        }

        void OnWebSocketNewData(object sender, WebSocketHandlers.ClientData clientData)
        {
            throw new NotImplementedException();
        }

        void OnWebSocketDataSent(object sender, string clientID)
        {
            throw new NotImplementedException();
        }
    }
}
