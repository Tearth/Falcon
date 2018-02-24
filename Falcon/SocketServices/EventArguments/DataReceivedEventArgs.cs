using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    public class DataReceivedEventArgs : EventArgs
    {
        public Socket Socket { get; }
        public byte[] Data { get; }

        public DataReceivedEventArgs(Socket socket, byte[] data)
        {
            Socket = socket;
            Data = data;
        }
    }
}
