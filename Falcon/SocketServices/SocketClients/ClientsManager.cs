using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketClients
{
    class ClientsManager
    {
        List<Client> clients;

        public ClientsManager()
        {
            clients = new List<Client>();
        }

        public void Add(Client client)
        {
            clients.Add(client);
        }

        public void Remove(Client client)
        {
            clients.Remove(client);
        }

        public Client Get(String id)
        {
            return clients.FirstOrDefault(p => p.ID == id);
        }

        public bool Exists(String id)
        {
            return clients.Exists(p => p.ID == id);
        }
    }
}
