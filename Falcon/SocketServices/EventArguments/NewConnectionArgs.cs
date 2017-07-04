using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.EventArguments
{
    class NewConnectionArgs : EventArgs
    {
        public Client Client { get; private set; }

        public NewConnectionArgs(Client client)
        {
            this.Client = client;
        }
    }
}
