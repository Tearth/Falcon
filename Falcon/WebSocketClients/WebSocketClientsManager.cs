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
        private readonly IDictionary<string, WebSocketClient> _webSocketClients;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketClientsManager"/> class.
        /// </summary>
        public WebSocketClientsManager()
        {
            _webSocketClients = new ConcurrentDictionary<string, WebSocketClient>();
        }

        /// <inheritdoc />
        public void Add(WebSocketClient client)
        {
            _webSocketClients.Add(client.Id, client);
        }

        /// <inheritdoc />
        public void Remove(WebSocketClient client)
        {
            _webSocketClients.Remove(client.Id);
        }

        /// <inheritdoc />
        public WebSocketClient GetById(string id)
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
