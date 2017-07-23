using System;
using System.Collections.Concurrent;

namespace Falcon.WebSocketClients
{
    class WebSocketClientsManager
    {
        ConcurrentDictionary<string, WebSocketClient> webSocketClients;

        public WebSocketClientsManager()
        {
            this.webSocketClients = new ConcurrentDictionary<string, WebSocketClient>();
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

        public WebSocketClient Get(string id)
        {
            if (!Exists(id))
                return null;

            return webSocketClients[id];
        }

        public bool Exists(string id)
        {
            return webSocketClients.ContainsKey(id);
        }
    }
}
