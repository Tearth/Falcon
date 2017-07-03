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
        Socket socket;

        public Client()
        {

        }

        public void Bind(Socket socket)
        {
            this.socket = socket;
        }
    }
}
