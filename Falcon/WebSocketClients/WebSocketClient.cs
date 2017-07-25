using System;
using System.Linq;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    internal class WebSocketClient
    {
        public string ID { get; private set; }
        public Socket Socket { get; private set; }
        public bool HandshakeDone { get; set; }
        public Buffer Buffer { get; private set; }

        public WebSocketClient(Socket socket, int bufferSize)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;
            Buffer = new Buffer(bufferSize);
        }
    }
}
