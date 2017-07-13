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
            var serializedFrameLength = frame.HeaderLength + frame.PayloadLength;
            var bytes = new byte[serializedFrameLength];

            bytes[0] = (byte)(128 + frame.OpCode);
            bytes[1] = frame.PayloadLengthSignature;

            var lengthBytes = BitConverter.GetBytes((ulong)frame.Payload.Length);
            var lengthBytesCount = frame.HeaderLength - 2;

            Array.Copy(lengthBytes, 0, bytes, 2, lengthBytesCount);
            Array.Copy(frame.Payload, 0, bytes, 2 + lengthBytesCount, frame.Payload.Length);

            return bytes;
        }
    }
}
