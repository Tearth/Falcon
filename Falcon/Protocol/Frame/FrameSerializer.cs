using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class FrameSerializer
    {
        public byte[] GetBytes(WebSocketFrame frame)
        {
            var serializedFrameLength = GetSerializedFrameLength(frame.Payload.Length);
            var bytes = new byte[serializedFrameLength];

            bytes[0] = (byte)(128 + frame.OpCode);
            bytes[1] = GetFirstByteOfPayloadLength(frame.Payload.Length);

            var lengthBytes = BitConverter.GetBytes((ulong)frame.Payload.Length);
            var lengthBytesCount = GetBytesCountOfExtendedPayloadLength(frame.Payload.Length);

            Array.Copy(lengthBytes, 0, bytes, 2, lengthBytesCount);
            Array.Copy(frame.Payload, 0, bytes, 2 + lengthBytesCount, frame.Payload.Length);

            return bytes;
        }

        byte GetFirstByteOfPayloadLength(int payloadLength)
        {
            if (payloadLength > 65535)
                return 127;
            else if (payloadLength > 126)
                return 126;

            return (byte)payloadLength;
        }

        byte GetBytesCountOfExtendedPayloadLength(int payloadLength)
        {
            if (payloadLength > 65535)
                return 8;
            else if (payloadLength > 126)
                return 2;

            return 0;
        }

        int GetSerializedFrameLength(int payloadLength)
        {
            var payloadLengthByte = GetFirstByteOfPayloadLength(payloadLength);

            if (payloadLengthByte == 127)
                return (10 + payloadLength);
            else if (payloadLengthByte == 126)
                return (4 + payloadLength);

            return (2 + payloadLength);
        }
    }
}
