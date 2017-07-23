using System;
using System.Linq;

namespace Falcon.Protocol.Frame
{
    class FrameSerializer
    {
        public byte[] GetBytes(WebSocketFrame frame)
        {
            var bytes = new byte[frame.FrameLength];

            bytes[0] = (byte)((Convert.ToByte(frame.FIN) << 7) + frame.OpCode);
            bytes[1] = frame.PayloadLengthSignature;

            var lengthBytes = BitConverter.GetBytes((ulong)frame.Payload.Length);
            var lengthBytesCount = frame.HeaderLength - 2;
            var lengthBytesReversed = lengthBytes.Take(lengthBytesCount).Reverse().ToArray();

            Array.Copy(lengthBytesReversed, 0, bytes, 2, lengthBytesCount);
            Array.Copy(frame.Payload, 0, bytes, 2 + lengthBytesCount, (int)frame.PayloadLength);

            return bytes;
        }
    }
}
