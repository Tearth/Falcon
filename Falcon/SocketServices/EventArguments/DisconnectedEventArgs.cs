﻿using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    internal class DisconnectedEventArgs : EventArgs
    {
        public Socket Socket { get; private set; }
        public bool Unexpected { get { return Exception != null; } }
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
