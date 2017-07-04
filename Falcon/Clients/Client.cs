using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Clients
{
    class Client
    {
        public String ID { get; set; }
        public Socket Socket { get; private set; }

        public int BufferSize { get; private set; }
        public byte[] ReceivedData { get; set; }
        public byte[] Buffer { get; set; }

        public Client()
        {
            this.ID = String.Empty;
            this.BufferSize = 8 * 1024;
            this.Buffer = new byte[this.BufferSize];
        }

        public void Bind(Socket socket)
        {
            this.Socket = socket;

            this.ID = Guid.NewGuid().ToString();
        }
    }
}
