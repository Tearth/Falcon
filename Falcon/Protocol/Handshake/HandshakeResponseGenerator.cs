using System;
using System.Linq;
using System.Text;

namespace Falcon.Protocol.Handshake
{
    class HandshakeResponseGenerator
    {
        HandshakeParser handshakeParser;
        HandshakeKeyGenerator handshakeKeyGenerator;

        const String webSocketKeyName = "Sec-WebSocket-Key";
        const String endSequence = "\r\n\r\n";

        public HandshakeResponseGenerator()
        {
            this.handshakeParser = new HandshakeParser();
            this.handshakeKeyGenerator = new HandshakeKeyGenerator();
        }

        public byte[] GetResponse(byte[] request)
        {
            var data = ASCIIEncoding.UTF8.GetString(request);

            if (!data.Contains(endSequence))
                return null;

            var handshakeFields = handshakeParser.ParseToDictionary(data);
            if (!handshakeFields.Any(p => p.Key == webSocketKeyName))
                return null;

            var key = handshakeFields[webSocketKeyName];
            var responseKey = handshakeKeyGenerator.Get(key);

            var response = String.Format("HTTP/1.1 101 Switching Protocols\r\n" +
                                         "Upgrade: websocket\r\n" +
                                         "Connection: Upgrade\r\n" +
                                         "Sec-WebSocket-Accept: {0}" +
                                         "{1}",
                                          responseKey, endSequence);

            return ASCIIEncoding.UTF8.GetBytes(response);
        }
    }
}
