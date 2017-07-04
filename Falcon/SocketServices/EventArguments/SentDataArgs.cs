using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.EventArguments
{
    class SentDataArgs : EventArgs
    {
        public Client Client { get; private set; }
        public int BytesSent { get; private set; }

        public SentDataArgs(Client client, int bytesSent)
        {
            this.Client = client;
            this.BytesSent = bytesSent;
        }
    }
}
