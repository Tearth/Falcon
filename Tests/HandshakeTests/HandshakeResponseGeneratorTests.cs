using Falcon.Protocol.Handshake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.HandshakeTests
{
    public class HandshakeResponseGeneratorTests
    {
        [Fact]
        public void GetResponse_ValidRequest_ValidResponse()
        {
            var responseGenerator = new HandshakeResponseGenerator();
            var request = "GET /chat HTTP/1.1\r\n" +
                          "Host: server.example.com\r\n" +
                          "Upgrade: websocket\r\n" +
                          "Connection: Upgrade\r\n" +
                          "Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==\r\n" +
                          "Origin: http://example.com\r\n" +
                          "Sec-WebSocket-Protocol: chat, superchat\r\n" +
                          "Sec-WebSocket-Version: 13\r\n" +
                          "\r\n";

            var requestBytes = Encoding.UTF8.GetBytes(request);
            var result = responseGenerator.GetResponse(requestBytes);
            var stringResult = Encoding.UTF8.GetString(result);
            
            Assert.Equal("HTTP/1.1 101 Switching Protocols\r\n" +
                         "Upgrade: websocket\r\n" +
                         "Connection: Upgrade\r\n" +
                         "Sec-WebSocket-Accept: HSmrc0sMlYUkAGmm5OPpG2HaGWk=\r\n" +
                         "\r\n", stringResult);
        }

        [Fact]
        public void GetResponse_PartialRequest_ZeroBytesResponse()
        {
            var responseGenerator = new HandshakeResponseGenerator();
            var request = "GET /chat HTTP/1.1\r\n" +
                          "Host: server.example.com\r\n" +
                          "Upgrade: websocket\r\n" +
                          "Connection: Upgrade\r\n";

            var requestBytes = Encoding.UTF8.GetBytes(request);
            var result = responseGenerator.GetResponse(requestBytes);

            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void GetResponse_RequestWithoutEndSequence_ZeroBytesResponse()
        {
            var responseGenerator = new HandshakeResponseGenerator();
            var request = "GET /chat HTTP/1.1\r\n" +
                          "Host: server.example.com\r\n" +
                          "Upgrade: websocket\r\n" +
                          "Connection: Upgrade\r\n" +
                          "Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==\r\n" +
                          "Origin: http://example.com\r\n" +
                          "Sec-WebSocket-Protocol: chat, superchat\r\n" +
                          "Sec-WebSocket-Version: 13\r\n";

            var requestBytes = Encoding.UTF8.GetBytes(request);
            var result = responseGenerator.GetResponse(requestBytes);

            Assert.Equal(0, result.Length);
        }
    }
}
