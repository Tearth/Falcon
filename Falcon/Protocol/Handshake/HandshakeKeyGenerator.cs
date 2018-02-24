using System;
using System.Security.Cryptography;
using System.Text;

namespace Falcon.Protocol.Handshake
{
    /// <summary>
    /// Represents a set of methods to generate a handshake key.
    /// </summary>
    public class HandshakeKeyGenerator
    {
        private const string MagicHashString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        /// <summary>
        /// Generates handshake key based on request key from the client.
        /// </summary>
        /// <param name="requestKey">The request key.</param>
        /// <returns>The handshake key in base64 form.</returns>
        public string Get(string requestKey)
        {
            var mergedKeys = requestKey + MagicHashString;
            var sha1Generator = SHA1.Create();

            var convertedKey = Encoding.UTF8.GetBytes(mergedKeys);
            var sha1Key = sha1Generator.ComputeHash(convertedKey);
            var base64Key = Convert.ToBase64String(sha1Key);

            return base64Key;
        }
    }
}
