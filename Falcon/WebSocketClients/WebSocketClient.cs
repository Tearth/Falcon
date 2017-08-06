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
            var remoteEndpoint = ((IPEndPoint)this.Socket.RemoteEndPoint);
            return new ClientInfo()
            {
                ClientID = this.ID,
                IP = remoteEndpoint.Address,
                Port = remoteEndpoint.Port,
                JoinTime = this.JoinTime
            };
        }
    }
}
