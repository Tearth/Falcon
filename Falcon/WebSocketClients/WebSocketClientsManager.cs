using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketClients
{
    class WebSocketClientsManager
    {
        List<WebSocketClient> webSocketClients;

        public WebSocketClientsManager()
        {

        }

        public void Add(WebSocketClient client)
        {
            webSocketClients.Add(client);
        }

        public void Remove(WebSocketClient client)
        {
            webSocketClients.Remove(client);
        }

        public WebSocketClient GetByID(String id)
        {
            return webSocketClients.FirstOrDefault(p => p.ID == id);
        }
    }
}
