using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Protocol.Handshake
{
    /// <summary>
    /// Represents a set of methods to parse HTTP handshake requests.
    /// </summary>
    public class HandshakeParser
    {
        private const string EndLineSequence = "\r\n";

        /// <summary>
        /// Splits HTTP handshake request into dictionary of records.
        /// </summary>
        /// <param name="request">The HTTP handshake request.</param>
        /// <returns>The dictionary of records.</returns>
        public IDictionary<string, string> ParseToDictionary(string request)
        {
            var requestFields = new Dictionary<string, string>();
            var lines = SplitRequest(request);

            foreach (var l in lines)
            {
                var tokens = l.Split(':');
                if (tokens.Length < 2)
                {
                    continue;
                }

                requestFields.Add(tokens[0].Trim(), tokens[1].Trim());
            }
            return requestFields;
        }

        private IList<string> SplitRequest(string request)
        {
            return request.Split(EndLineSequence.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
