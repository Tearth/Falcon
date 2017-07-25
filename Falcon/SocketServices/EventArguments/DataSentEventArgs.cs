using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    internal class DataSentEventArgs : EventArgs
    {
        public Socket Socket { get; private set; }
        public int BytesSent { get; private set; }

        public DataSentEventArgs(Socket socket, int bytesSent)
        {
            Socket = socket;
            BytesSent = bytesSent;
        }
    }
}
