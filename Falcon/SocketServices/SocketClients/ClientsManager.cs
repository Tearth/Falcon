using System;
using System.Collections.Concurrent;

namespace Falcon.SocketClients
{
    class ClientsManager
    {
        ConcurrentDictionary<string, Client> clients;

        public ClientsManager()
        {
            this.clients = new ConcurrentDictionary<string, Client>();
        }

        public void Add(Client client)
        {
            clients.TryAdd(client.ID, client);
        }

        public void Remove(Client client)
        {
            Client outValue;
            clients.TryRemove(client.ID, out outValue);
        }

        public Client Get(string id)
        {
            if (!Exists(id))
                return null;

            return clients[id];
        }

        public bool Exists(string id)
        {
            return clients.ContainsKey(id);
        }
    }
}
