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
            var request = @"GET /chat HTTP/1.1
                            Host: server.example.com
                            Upgrade: websocket
                            Connection: Upgrade
                            Sec-WebSocket-Key: dGhlIHNhbXBsZSBub25jZQ==
                            Origin: http://example.com
                            Sec-WebSocket-Protocol: chat, superchat
                            Sec-WebSocket-Version: 13
                            ";

            var result = parser.ParseToDictionary(request);

            Assert.True(result["Sec-WebSocket-Key"] == "dGhlIHNhbXBsZSBub25jZQ==");
        }

        [Fact]
        public void ParseToDictionary_RequestWithoutKey_ResultHasNoKey()
        {
            var parser = new HandshakeParser();
            var request = @"GET /chat HTTP/1.1
                            Host: server.example.com
                            Upgrade: websocket
                            Connection: Upgrade
                            Origin: http://example.com
                            Sec-WebSocket-Protocol: chat, superchat
                            Sec-WebSocket-Version: 13
                            ";

            var result = parser.ParseToDictionary(request);

            Assert.False(result.ContainsKey("Sec-WebSocket-Key"));
        }
    }
}
