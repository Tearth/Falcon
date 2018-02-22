using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    public class WebSocketClientsManager : IWebSocketClientsManager
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

        public WebSocketClient GetByID(string id)
        {
            return !Exists(id) ? null : _webSocketClients[id];
        }

        public WebSocketClient GetBySocket(Socket socket)
        {
            return _webSocketClients.FirstOrDefault(p => p.Value.Socket == socket).Value;
        }

        public bool Exists(string id)
        {
            return _webSocketClients.ContainsKey(id);
        }
    }
}
