using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    /// <summary>
    /// Represents a set of methods to manage WebSocket clients.
    /// </summary>
    public class WebSocketClientsManager : IWebSocketClientsManager
    {
        private IDictionary<string, WebSocketClient> _webSocketClients;

        public WebSocketClientsManager()
        {
            _webSocketClients = new ConcurrentDictionary<string, WebSocketClient>();
        }

        /// <inheritdoc />
        public void Add(WebSocketClient client)
        {
            _webSocketClients.Add(client.ID, client);
        }

        /// <inheritdoc />
        public void Remove(WebSocketClient client)
        {
            _webSocketClients.Remove(client.ID);
        }

        /// <inheritdoc />
        public WebSocketClient GetByID(string id)
        {
            return !Exists(id) ? null : _webSocketClients[id];
        }

        /// <inheritdoc />
        public WebSocketClient GetBySocket(Socket socket)
        {
            return _webSocketClients.FirstOrDefault(p => p.Value.Socket == socket).Value;
        }

        /// <inheritdoc />
        public bool Exists(string id)
        {
            return _webSocketClients.ContainsKey(id);
        }
    }
}
