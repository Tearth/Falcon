using Falcon.SocketClients;
using System;

namespace Falcon.SocketServices.EventArguments
{
    class DataSentEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public int BytesSent { get; private set; }

        public DataSentEventArgs(Client client, int bytesSent)
        {
            Client = client;
            BytesSent = bytesSent;
        }
    }
}
