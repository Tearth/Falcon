using Falcon.WebSocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.WebSocketClientTests
{
    public class WebSocketClientsManagerTests
    {
        [Fact]
        public void Add_ValidClient_ClientExists()
        {
            var clientsManager = new WebSocketClientsManager();
            var client = new WebSocketClient(null, 16);

            clientsManager.Add(client);

            Assert.NotNull(clientsManager.GetByID(client.ID));
        }

        [Fact]
        public void Remove_ValidClient_ClientNotExists()
        {
            var clientsManager = new WebSocketClientsManager();
            var client = new WebSocketClient(null, 16);

            clientsManager.Add(client);
            clientsManager.Remove(client);

            Assert.Null(clientsManager.GetByID(client.ID));
        }

        [Fact]
        public void Exists_ValidClient_TrueResult()
        {
            var clientsManager = new WebSocketClientsManager();
            var client = new WebSocketClient(null, 16);

            clientsManager.Add(client);

            Assert.True(clientsManager.Exists(client.ID));
        }

        [Fact]
        public void Exists_InvalidClient_FalseResult()
        {
            var clientsManager = new WebSocketClientsManager();
            var client = new WebSocketClient(null, 16);

            Assert.False(clientsManager.Exists(client.ID));
        }
    }
}
