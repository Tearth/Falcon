using Falcon.Protocol.Handshake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Handshake
{
    public class HandshakeParserTests
    {
        [Fact]
        public void ParseToDictionary_ValidRequest_ValidKey()
        {
            var parser = new HandshakeParser();
            var request = "GET /chat HTTP/1.1\r\n" +
                          "Host: server.example.com\r\n" +
                          "Upgrade: websocket\r\n" +
                          "Connection: Upgrade\r\n" +
                          "Sec-WebSocket-Key: dGhlIHNhbXBsZSBub25jZQ==\r\n" + 
                          "Origin: http://example.com\r\n" +
                          "Sec-WebSocket-Protocol: chat, superchat\r\n" +
                          "Sec-WebSocket-Version: 13\r\n" +
                          "\r\n";

            var result = parser.ParseToDictionary(request);

            Assert.Equal("dGhlIHNhbXBsZSBub25jZQ==", result["Sec-WebSocket-Key"]);
        }

        [Fact]
        public void ParseToDictionary_RequestWithoutKey_ResultHasNoKey()
        {
            var parser = new HandshakeParser();
            var request = "GET /chat HTTP/1.1\r\n" +
                          "Host: server.example.com\r\n" +
                          "Upgrade: websocket\r\n" +
                          "Connection: Upgrade\r\n" +
                          "Origin: http://example.com\r\n" +
                          "Sec-WebSocket-Protocol: chat, superchat\r\n" +
                          "Sec-WebSocket-Version: 13\r\n" +
                          "\r\n";

            var result = parser.ParseToDictionary(request);

            Assert.False(result.ContainsKey("Sec-WebSocket-Key"));
        }
    }
}
