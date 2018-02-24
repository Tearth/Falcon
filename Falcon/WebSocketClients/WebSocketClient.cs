using System;
using System.Net;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    public class WebSocketClient
    {
        public string ID { get; }
        public Socket Socket { get; }
        public bool HandshakeDone { get; set; }
        public Buffer Buffer { get; }
        public DateTime JoinTime { get; }

        public WebSocketClient(Socket socket, uint bufferSize)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;
            Buffer = new Buffer(bufferSize);
            JoinTime = DateTime.Now;
        }

        public ClientInfo GetInfo()
        {
            var remoteEndpoint = (IPEndPoint)Socket.RemoteEndPoint;
            return new ClientInfo
            {
                ClientID = ID,
                IP = remoteEndpoint.Address,
                Port = remoteEndpoint.Port,
                JoinTime = JoinTime
            };
        }
    }
}
