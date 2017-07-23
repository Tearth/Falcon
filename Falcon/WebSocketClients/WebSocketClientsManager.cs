using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Falcon.WebSocketClients
{
    class WebSocketClientsManager
    {
        IDictionary<string, WebSocketClient> webSocketClients;

        public WebSocketClientsManager()
        {
            this.webSocketClients = new ConcurrentDictionary<string, WebSocketClient>();
        }

        public void Add(WebSocketClient client)
        {
            webSocketClients.Add(client.ID, client);
        }

        public void Remove(WebSocketClient client)
        {
            webSocketClients.Remove(client.ID);
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
