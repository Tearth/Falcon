using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    public class ConnectedEventArgs : EventArgs
    {
        public Socket Socket { get; }

        public ConnectedEventArgs(Socket socket)
        {
            Socket = socket;
        }
    }
}
