using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class FramesManager
    {
        FrameSerializer serializer;
        FrameDeserializer deserializer;

        public FramesManager()
        {
            serializer = new FrameSerializer();
            deserializer = new FrameDeserializer();
        }

        public byte[] Serialize(byte[] data)
        {
            var frame = new WebSocketFrame();
            frame.OpCode = 1;
            frame.Payload = data;

            return serializer.GetBytes(frame);
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
