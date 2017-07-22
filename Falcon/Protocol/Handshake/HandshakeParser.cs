using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Protocol.Handshake
{
    class HandshakeParser
    {
        const String endLineSequence = "\r\n";

        public HandshakeParser()
        {

        }

        public Dictionary<String, String> ParseToDictionary(String request)
        {
            var requestFields = new Dictionary<String, String>();
            var lines = SplitRequest(request);

            foreach (String l in lines)
            {
                var tokens = l.Split(':');
                if (tokens.Length < 2)
                    continue;

                requestFields.Add(tokens[0].Trim(), tokens[1].Trim());
            }
            return requestFields;
        }

        List<String> SplitRequest(String request)
        {
            return request.Split(endLineSequence.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
