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
            frame.FIN = true;
            frame.Mask = false;
            frame.Payload = data;
            frame.PayloadLengthSignature = GetPayloadLengthSignature(frame.Payload);
            frame.PayloadExtendedLength = GetPayloadExtendedLength(frame.Payload);

            return serializer.GetBytes(frame);
        }

        public byte[] Deserialize(byte[] data, out DecryptResult result, out int parsedBytes)
        {
            var frame = deserializer.GetFrame(data, out result);
            if (result != DecryptResult.SuccessWithFIN && result != DecryptResult.SuccessWithoutFIN)
            {
                parsedBytes = 0;
                return null;
            }

            parsedBytes = (int)frame.FrameLength;
            return frame.GetMessage();
        }

        byte GetPayloadLengthSignature(byte[] data)
        {
            if (data.Length > 65535)
                return 127;
            if (data.Length >= 126)
                return 126;

            return (byte)data.Length;
        }

        uint GetPayloadExtendedLength(byte[] data)
        {
            if (data.Length < 126)
                return 0;

            return (uint)data.Length;
        }
    }
}
