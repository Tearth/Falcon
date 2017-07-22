using System;
using System.Collections.Concurrent;

namespace Falcon.SocketClients
{
    class ClientsManager
    {
        ConcurrentDictionary<String, Client> clients;

        public ClientsManager()
        {
            clients = new ConcurrentDictionary<String, Client>();
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

        public Client Get(String id)
        {
            if (!Exists(id))
                return null;

            return clients[id];
        }

        public bool Exists(String id)
        {
            return clients.ContainsKey(id);
        }
    }
}
