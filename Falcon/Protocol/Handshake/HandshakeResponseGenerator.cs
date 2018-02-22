using System.Linq;
using System.Text;

namespace Falcon.Protocol.Handshake
{
    public class HandshakeResponseGenerator : IHandshakeResponseGenerator
    {
        private HandshakeParser _handshakeParser;
        private HandshakeKeyGenerator _handshakeKeyGenerator;

        private const string WebSocketKeyName = "Sec-WebSocket-Key";
        private const string EndSequence = "\r\n\r\n";

        public HandshakeResponseGenerator()
        {
            _handshakeParser = new HandshakeParser();
            _handshakeKeyGenerator = new HandshakeKeyGenerator();
        }

        public byte[] GetResponse(byte[] request)
        {
            var data = Encoding.UTF8.GetString(request);

            if (!data.Contains(EndSequence))
            {
                return new byte[0];
            }

            var handshakeFields = _handshakeParser.ParseToDictionary(data);
            if (handshakeFields.All(p => p.Key != WebSocketKeyName))
            {
                return new byte[0];
            }

            var key = handshakeFields[WebSocketKeyName];
            var responseKey = _handshakeKeyGenerator.Get(key);

            var response = string.Format("HTTP/1.1 101 Switching Protocols\r\n" +
                                         "Upgrade: websocket\r\n" +
                                         "Connection: Upgrade\r\n" +
                                         "Sec-WebSocket-Accept: {0}" +
                                         "{1}",
                                          responseKey, EndSequence);

            return Encoding.UTF8.GetBytes(response);
        }
    }
}
