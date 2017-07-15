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

        public byte[] Serialize(byte[] data, FrameType type)
        {
            var frame = new WebSocketFrame(data);
            frame.OpCode = (byte)type;
            frame.FIN = true;
            frame.Mask = false;

            return serializer.GetBytes(frame);
        }

        public byte[] Deserialize(byte[] data, out DecryptResult result, out FrameType type, out int parsedBytes)
        {
            var frame = deserializer.GetFrame(data, out result);
            if (result != DecryptResult.SuccessWithFIN && result != DecryptResult.SuccessWithoutFIN)
            {
                parsedBytes = 0;
                type = FrameType.None;
                return null;
            }

            parsedBytes = (int)frame.FrameLength;
            type = (FrameType)frame.OpCode;
            return frame.GetMessage();
        }
    }
}
