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
        public bool Unexpected { get { return Exception != null; } }
        public Exception Exception { get; private set; }

        public DisconnectedEventArgs(Client client)
        {
            this.Client = client;
        }

        public DisconnectedEventArgs(Client client, Exception exception) : this(client)
        {
            this.Exception = exception;
        }
    }
}
