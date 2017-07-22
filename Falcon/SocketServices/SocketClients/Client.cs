using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketClients
{
    class Client
    {
        public String ID { get; private set; }
        public Socket Socket { get; private set; }
        public byte[] Buffer { get; private set; }
        public DateTime JoinTime { get; private set; }

        public Client(Socket socket, int bufferSize)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Buffer = new byte[bufferSize];
            this.Socket = socket;

            this.JoinTime = DateTime.Now;
        }
    }
}
