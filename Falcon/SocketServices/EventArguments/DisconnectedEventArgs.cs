using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.EventArguments
{
    class DisconnectedEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public bool Unexpected { get; private set; }

        public DisconnectedEventArgs(Client client, bool unexpected)
        {
            this.Client = client;
            this.Unexpected = unexpected;
        }
    }
}
