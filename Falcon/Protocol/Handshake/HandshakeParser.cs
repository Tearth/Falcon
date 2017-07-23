using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Protocol.Handshake
{
    class HandshakeParser
    {
        const string endLineSequence = "\r\n";

        public HandshakeParser()
        {

        }

        public Dictionary<string, string> ParseToDictionary(string request)
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

        List<string> SplitRequest(string request)
        {
            return request.Split(endLineSequence.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
