using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    enum FrameType : byte
    {
        None = 0x00,
        Message = 0x01,
        Disconnect = 0x8,
        Ping = 0x9,
        Pong = 0xA
    }
}
