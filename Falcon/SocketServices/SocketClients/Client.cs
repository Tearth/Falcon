﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketClients
{
    class Client
    {
        public String ID { get; private set; }
        public Socket Socket { get; private set; }

        public int BufferSize { get; private set; }
        public byte[] Buffer { get; private set; }

        public Client(Socket socket)
        {
            this.ID = Guid.NewGuid().ToString();
            this.BufferSize = 8 * 1024;
            this.Buffer = new byte[this.BufferSize];
            this.Socket = socket;
        }
    }
}
