using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    internal interface IFramesManager
    {
        byte[] Serialize(byte[] data, FrameType type);
        byte[] Deserialize(byte[] data, out DeserializeResult result, out FrameType type, out int parsedBytes);
    }
}
