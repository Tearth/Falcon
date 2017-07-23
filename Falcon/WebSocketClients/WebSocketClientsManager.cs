using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Falcon.WebSocketClients
{
    class WebSocketClientsManager
    {
        IDictionary<string, WebSocketClient> _webSocketClients;

        public WebSocketClientsManager()
        {
            _webSocketClients = new ConcurrentDictionary<string, WebSocketClient>();
        }

        public void Add(WebSocketClient client)
        {
            _webSocketClients.Add(client.ID, client);
        }

        public void Remove(WebSocketClient client)
        {
            _webSocketClients.Remove(client.ID);
        }

        public WebSocketClient Get(string id)
        {
            if (!Exists(id))
                return null;

            return _webSocketClients[id];
        }

        public bool Exists(string id)
        {
            return _webSocketClients.ContainsKey(id);
        }
    }
}
