using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    internal class WebSocketClient
    {
        public string ID { get; private set; }
        public Socket Socket { get; private set; }
        public bool HandshakeDone { get; set; }
        public Buffer Buffer { get; private set; }
        public DateTime JoinTime { get; private set; }

        public WebSocketClient(Socket socket, uint bufferSize)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;
            Buffer = new Buffer(bufferSize);
            JoinTime = DateTime.Now;
        }

        public ClientInfo GetInfo()
        {
            var removeEndpoint = ((IPEndPoint)this.Socket.RemoteEndPoint);
            return new ClientInfo()
            {
                ClientID = this.ID,
                IP = removeEndpoint.Address,
                Port = removeEndpoint.Port,
                JoinTime = this.JoinTime
            };
        }
    }
}
