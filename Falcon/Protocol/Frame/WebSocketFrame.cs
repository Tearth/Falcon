namespace Falcon.Protocol.Frame
{
    internal class WebSocketFrame
    {
        public bool FIN { get; set; }
        public byte OpCode { get; set; }
        public bool Mask { get; private set; }

        public byte PayloadLengthSignature { get; set; }
        public ulong PayloadExtendedLength { get; set; }
        public byte HeaderLength
        {
            get
            {
                var maskLength = Mask ? MaskingKey.Length : 0;

                if (PayloadLengthSignature < 126)
                {
                    return (byte)(2 + maskLength);
                }

                if (PayloadLengthSignature == 126)
                {
                    return (byte)(4 + maskLength);
                }

                return (byte)(10 + maskLength);
            }
        }
        public ulong PayloadLength => PayloadLengthSignature < 126 ? PayloadLengthSignature : PayloadExtendedLength;

        public ulong FrameLength => HeaderLength + PayloadLength;

        public byte[] MaskingKey { get; set; }
        public byte[] Payload { get; set; }

        public WebSocketFrame(bool mask)
        {
            Mask = mask;
        }

        public WebSocketFrame(byte[] data, bool mask) : this(mask)
        {
            Payload = data;

            if (data.Length > 65535)
            {
                PayloadLengthSignature = 127;
            }
            else if (data.Length >= 126)
            {
                PayloadLengthSignature = 126;
            }
            else
            {
                PayloadLengthSignature = (byte)data.Length;
            }

            PayloadExtendedLength = PayloadLengthSignature < 126 ? 0 : (ulong)data.Length;
        }

        public byte[] GetMessage()
        {
            if (!Mask)
            {
                return Payload;
            }

            var message = new byte[PayloadLength];
            for (ulong i = 0; i < PayloadLength; i++)
            {
                message[i] = (byte)(Payload[i] ^ MaskingKey[i % 4]);
            }

            return message;
        }
    }
}
