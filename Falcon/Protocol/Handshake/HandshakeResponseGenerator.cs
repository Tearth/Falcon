using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Handshake
{
    class HandshakeResponseGenerator
    {
        const String endSequence = "\r\n\r\n";
        const String webSocketKeyName = "Sec-WebSocket-Key";
        const String magicHashString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public byte[] GetResponse(byte[] request)
        {
            var data = ASCIIEncoding.UTF8.GetString(request);

            if (!data.Contains(endSequence))
                return null;

            var fields = ParseRequestToDictionary(data);
            var key = fields[webSocketKeyName];
            var responseKey = CreateResponseKey(key);

            var response = String.Format("HTTP/1.1 101 Switching Protocols\r\n" +
                                         "Upgrade: websocket\r\n" +
                                         "Connection: Upgrade\r\n" +
                                         "Sec-WebSocket-Accept: {0}" +
                                         "{1}",
                                          responseKey, endSequence);
            return ASCIIEncoding.UTF8.GetBytes(response);
        }

        Dictionary<String, String> ParseRequestToDictionary(String request)
        {
            var requestFields = new Dictionary<String, String>();
            var lines = request.Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (String l in lines)
            {
                var tokens = l.Split(':');
                if (tokens.Length < 2)
                    continue;

                requestFields.Add(tokens[0].Trim(), tokens[1].Trim());
            }
            return requestFields;
        }

        String CreateResponseKey(String requestKey)
        {
            var mergedKeys = requestKey + magicHashString;
            var sha1Generator = SHA1.Create();

            var convertedKey = Encoding.UTF8.GetBytes(mergedKeys);
            var sha1Key = sha1Generator.ComputeHash(convertedKey);
            var base64Key = Convert.ToBase64String(sha1Key);

            return base64Key;
        }
    }
}
