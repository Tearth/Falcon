using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Protocol.Handshake
{
    class HandshakeParser
    {
        const string EndLineSequence = "\r\n";

        public IDictionary<string, string> ParseToDictionary(string request)
        {
            var requestFields = new Dictionary<string, string>();
            var lines = SplitRequest(request);

            foreach (string l in lines)
            {
                var tokens = l.Split(':');
                if (tokens.Length < 2)
                    continue;

                requestFields.Add(tokens[0].Trim(), tokens[1].Trim());
            }
            return requestFields;
        }

        IList<string> SplitRequest(string request)
        {
            return request.Split(EndLineSequence.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
