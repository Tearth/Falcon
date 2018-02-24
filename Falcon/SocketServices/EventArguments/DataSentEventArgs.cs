using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    public class DataSentEventArgs : EventArgs
    {
        public Socket Socket { get; }
        public int BytesSent { get; }

        public DataSentEventArgs(Socket socket, int bytesSent)
        {
            Socket = socket;
            BytesSent = bytesSent;
        }
    }
}
