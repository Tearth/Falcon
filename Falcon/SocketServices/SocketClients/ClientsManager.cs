using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Falcon.SocketClients
{
    class ClientsManager
    {
        IDictionary<string, Client> _clients;

        public ClientsManager()
        {
            _clients = new ConcurrentDictionary<string, Client>();
        }

        public void Add(Client client)
        {
            _clients.Add(client.ID, client);
        }

        public void Remove(Client client)
        {
            _clients.Remove(client.ID);
        }

        public Client Get(string id)
        {
            if (!Exists(id))
                return null;

            return _clients[id];
        }

        public bool Exists(string id)
        {
            return _clients.ContainsKey(id);
        }
    }
}
