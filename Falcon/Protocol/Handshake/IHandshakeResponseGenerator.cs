using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Handshake
{
    internal interface IHandshakeResponseGenerator
    {
        byte[] GetResponse(byte[] request);
    }
}
