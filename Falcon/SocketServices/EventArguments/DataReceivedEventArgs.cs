using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.EventArguments
{
    class DataReceivedEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public int BytesReceived { get; private set; }

        public DataReceivedEventArgs(Client client, int bytesReceived)
        {
            this.Client = client;
            this.BytesReceived = bytesReceived;
        }
    }
}
