using Falcon.SocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.ClientInformations
{
    class ClientInfoGenerator
    {
        public ClientInfoGenerator()
        {

        }

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
