using Falcon.SocketClients;
using System.Net;

namespace Falcon.SocketServices.ClientInformations
{
    class ClientInfoGenerator
    {
        public ClientInfo Get(Client client)
        {
            return new ClientInfo()
            {
                ClientID = client.ID,
                IP = ((IPEndPoint)client.Socket.RemoteEndPoint).Address,
                Port = ((IPEndPoint)client.Socket.RemoteEndPoint).Port,
                JoinTime = client.JoinTime
            };
        }
    }
}
