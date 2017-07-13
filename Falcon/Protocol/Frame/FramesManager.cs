using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class FramesManager
    {
        FrameDeserializer deserializer;

        public FramesManager()
        {
            deserializer = new FrameDeserializer();
        }

        public byte[] Serialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Deserialize(byte[] data, out DecryptResult result)
        {
            var frame = deserializer.GetFrame(data, out result);
            if (result != DecryptResult.SuccessWithFIN && result != DecryptResult.SuccessWithoutFIN)
                return null;

            return frame.GetMessage();
        }
    }
}
