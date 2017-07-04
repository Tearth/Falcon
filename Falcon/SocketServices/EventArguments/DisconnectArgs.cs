using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.EventArguments
{
    class DisconnectArgs : EventArgs
    {
        public Client Client { get; private set; }
        public bool Unexpected { get; private set; }

        public DisconnectArgs(Client client, bool unexpected)
        {
            this.Client = client;
            this.Unexpected = unexpected;
        }
    }
}
