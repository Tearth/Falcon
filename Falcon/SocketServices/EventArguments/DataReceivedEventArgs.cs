using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    public class DataReceivedEventArgs : EventArgs
    {
        public Socket Socket { get; private set; }
        public byte[] Data { get; private set; }

        public DataReceivedEventArgs(Socket socket, byte[] data)
        {
            Socket = socket;
            Data = data;
        }
    }
}
