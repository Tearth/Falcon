using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    public class DisconnectedEventArgs : EventArgs
    {
        public Socket Socket { get; private set; }
        public bool Unexpected => Exception != null;
        public Exception Exception { get; private set; }

        public DisconnectedEventArgs(Socket socket)
        {
            Socket = socket;
        }

        public DisconnectedEventArgs(Socket socket, Exception exception) : this(socket)
        {
            Exception = exception;
        }
    }
}
