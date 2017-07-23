using System;
using System.Net.Sockets;

namespace Falcon.SocketClients
{
    class Client
    {
        public string ID { get; private set; }
        public Socket Socket { get; private set; }
        public byte[] Buffer { get; private set; }
        public DateTime JoinTime { get; private set; }

        public bool Closed { get; set; }

        public Client(Socket socket, int bufferSize)
        {
            ID = Guid.NewGuid().ToString();
            Buffer = new byte[bufferSize];
            Socket = socket;

            JoinTime = DateTime.Now;
            Closed = false;
        }

        public void Close()
        {
            Closed = true;
            Socket.Close();
        }
    }
}
