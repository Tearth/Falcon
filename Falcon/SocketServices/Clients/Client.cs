using System.Net.Sockets;

namespace Falcon.SocketServices.Clients
{
    public class Client
    {
        public Socket Socket { get; }
        public byte[] Buffer { get; }

        public Client(Socket socket, int bufferSize)
        {
            Buffer = new byte[bufferSize];
            Socket = socket;
        }
    }
}
