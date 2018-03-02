using System;
using System.Net;
using Falcon;
using Falcon.Exceptions;
using Xunit;

namespace Tests
{
    public class WebSocketServerTests
    {
        [Fact]
        public void Start_OneCall_NoException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            var ex = Record.Exception(() => webSocketServer.Start(IPAddress.Any, 12345));

            Assert.Null(ex);
        }

        [Fact]
        public void Start_DoubleCall_ServerAlreadyWorkingException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            webSocketServer.Start(IPAddress.Any, 12345);

            Assert.Throws<ServerAlreadyWorkingException>(() => webSocketServer.Start(IPAddress.Any, 12345));
        }

        [Fact]
        public void Stop_OneCall_NoException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            webSocketServer.Start(IPAddress.Any, 12345);
            var ex = Record.Exception(() => webSocketServer.Stop());

            Assert.Null(ex);
        }

        [Fact]
        public void Stop_DoubleCall_ServerAlreadyClosedException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            webSocketServer.Start(IPAddress.Any, 12345);
            webSocketServer.Stop();

            Assert.Throws<ServerAlreadyClosedException>(() => webSocketServer.Stop());
        }

        [Fact]
        public void Send_ClientIDNotExists_ReturnsFalse()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            webSocketServer.Start(IPAddress.Any, 12345);
            var result = webSocketServer.SendData("strange-id", new byte[] {0, 1, 2});

            Assert.False(result);
        }

        [Fact]
        public void Send_ServerAlreadyClosed_ServerAlreadyClosedException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            Assert.Throws<ServerAlreadyClosedException>(() => webSocketServer.SendData("strange-id", new byte[] { 0, 1, 2 }));
        }
    }
}
