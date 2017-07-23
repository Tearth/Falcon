using Falcon.SocketClients;
using System;

namespace Falcon.SocketServices.EventArguments
{
    class DisconnectedEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public bool Unexpected { get { return Exception != null; } }
        public Exception Exception { get; private set; }

        public DisconnectedEventArgs(Client client)
        {
            Client = client;
        }

        public DisconnectedEventArgs(Client client, Exception exception) : this(client)
        {
            Exception = exception;
        }
    }
}
