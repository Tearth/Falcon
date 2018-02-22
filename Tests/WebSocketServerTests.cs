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
        public void Start_NullAddress_ArgumentNullException()
        {
            var fakeServerListener = new FakeServerListener();
            var webSocketServer = new WebSocketServer(1024, fakeServerListener);

            Assert.Throws<ArgumentNullException>("address", () => webSocketServer.Start(null, 12345));
        }

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
    }
}
