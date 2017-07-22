using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketClients
{
    class WebSocketClientsManager
    {
        ConcurrentDictionary<String, WebSocketClient> webSocketClients;

        public WebSocketClientsManager()
        {
            webSocketClients = new ConcurrentDictionary<String, WebSocketClient>();
        }

        public void Add(WebSocketClient client)
        {
            webSocketClients.TryAdd(client.ID, client);
        }

        public void Remove(WebSocketClient client)
        {
            WebSocketClient outValue;
            webSocketClients.TryRemove(client.ID, out outValue);
        }

        public WebSocketClient Get(String id)
        {
            if (!Exists(id))
                return null;

            return webSocketClients[id];
        }

        public bool Exists(String id)
        {
            return webSocketClients.ContainsKey(id);
        }
    }
}
