using System;
using System.Linq;

namespace Falcon.WebSocketClients
{
    class WebSocketClient
    {
        public string ID { get; private set; }
        public bool HandshakeDone { get; set; }
        public Buffer Buffer { get; private set; }

        public WebSocketClient(string id, int bufferSize)
        {
            ID = id;

            Buffer = new Buffer(bufferSize);
        }
    }
}
