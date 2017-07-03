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

        public Client()
        {

        }

        public void Bind(Socket socket)
        {
            this.Socket = socket;

            this.ID = Guid.NewGuid().ToString();
        }
    }
}
