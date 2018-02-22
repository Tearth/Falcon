using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    public interface IWebSocketClientsManager
    {
        void Add(WebSocketClient client);
        void Remove(WebSocketClient client);
        WebSocketClient GetByID(string id);
        WebSocketClient GetBySocket(Socket socket);
        bool Exists(string id);
    }
}
