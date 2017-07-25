﻿using System.Net.Sockets;

namespace Falcon.SocketClients
{
    internal class Client
    {
        public Socket Socket { get; private set; }
        public byte[] Buffer { get; private set; }

        public Client(Socket socket, int bufferSize)
        {
            Buffer = new byte[bufferSize];
            Socket = socket;
        }
    }
}
