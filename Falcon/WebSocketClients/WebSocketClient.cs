using Falcon.SocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketClients
{
    class WebSocketClient
    {
        public String ID { get; private set; }
        public byte[] Buffer { get; set; }
        public bool HandshakeDone { get; set; }

        public WebSocketClient(String id, int bufferSize)
        {
            this.ID = id;
            this.Buffer = new byte[bufferSize];
        }
    }
}
