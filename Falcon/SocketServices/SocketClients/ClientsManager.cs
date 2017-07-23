using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Falcon.SocketClients
{
    class ClientsManager
    {
        IDictionary<string, Client> clients;

        public ClientsManager()
        {
            this.clients = new ConcurrentDictionary<string, Client>();
        }

        public void Add(Client client)
        {
            clients.Add(client.ID, client);
        }

        public void Remove(Client client)
        {
            clients.Remove(client.ID);
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
