using System;
using System.Security.Cryptography;
using System.Text;

namespace Falcon.Protocol.Handshake
{
    class HandshakeKeyGenerator
    {
        const string magicHashstring = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public string Get(string requestKey)
        {
            var mergedKeys = requestKey + magicHashstring;
            var sha1Generator = SHA1.Create();

            var convertedKey = Encoding.UTF8.GetBytes(mergedKeys);
            var sha1Key = sha1Generator.ComputeHash(convertedKey);
            var base64Key = Convert.ToBase64String(sha1Key);

            return base64Key;
        }
    }
}
