using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    class ConnectedEventArgs : EventArgs
    {
        public Socket ClientSocket { get; private set; }

        public ConnectedEventArgs(Socket clientSocket)
        {
            this.ClientSocket = clientSocket;
        }
    }
}
