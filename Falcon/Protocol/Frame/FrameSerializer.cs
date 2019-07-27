using System;
using System.Linq;

namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents a set of methods to serialize frames.
    /// </summary>
    public class FrameSerializer
    {
        /// <summary>
        /// Serializes frame object into array of bytes.
        /// </summary>
        /// <param name="frame">The frame to serialize.</param>
        /// <returns>The serialized array of bytes.</returns>
        public byte[] GetBytes(WebSocketFrame frame)
        {
            var bytes = new byte[frame.FrameLength];

            bytes[0] = (byte)((Convert.ToByte(frame.Fin) << 7) + frame.OpCode);
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
