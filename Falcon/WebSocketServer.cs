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
        Connection server;

        public WebSocketServer()
        {
            server = new Connection();
        }

        public void Open(IPAddress address, short port)
        {
            var endPoint = new IPEndPoint(address, port);
            server.StartListening(endPoint);
        }

        public void Close()
        {
            server.StopListening();
        }
    }
}
