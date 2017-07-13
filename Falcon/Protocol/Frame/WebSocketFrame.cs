using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class WebSocketFrame
    {
        public bool FIN { get; set; }
        public byte OpCode { get; set; }
        public bool Mask { get; set; }

        public byte PayloadLengthSignature { get; set; }
        public ulong PayloadExtendedLength { get; set; }
        public byte HeaderLength
        {
            get
            {
                if (PayloadLengthSignature < 126) return 2;
                if (PayloadLengthSignature == 126) return 4;
                return 10;
            }
        }
        public ulong PayloadLength
        {
            get
            {
                return PayloadLengthSignature < 126 ? PayloadLengthSignature : PayloadExtendedLength;
            }
        }

        public ulong FrameLength
        {
            get
            {
                return HeaderLength + PayloadLength;
            }
        }

        public byte[] MaskingKey { get; set; }
        public byte[] Payload { get; set; }

        public byte[] GetMessage()
        {
            var message = new byte[PayloadLength];
            for (ulong i = 0; i < PayloadLength; i++)
            {
                message[i] = (byte)(Payload[i] ^ MaskingKey[i % 4]);
            }

            return message;
        }
    }
}
