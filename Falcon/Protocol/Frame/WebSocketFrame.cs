namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents a WebSocket frame and set of methods to manage.
    /// </summary>
    public class WebSocketFrame
    {
        /// <summary>
        /// Gets or sets the FIN bit (indicates whether the frame is the last from the sequence or not).
        /// </summary>
        public bool FIN { get; set; }

        /// <summary>
        /// Gets or sets the operation code (see more in <see cref="FrameType"/>).
        /// </summary>
        public byte OpCode { get; set; }

        /// <summary>
        /// Gets the mask bit (indicates whether the frame has encrypted payload with the specified mask or not).
        /// </summary>
        public bool Mask { get; }

        /// <summary>
        /// Gets or sets the payload length signature (less than 126 = use this as payload length,
        /// equal to 126 = use additional two bytes for the payload length, equal to 127 = use additional
        /// four bytes for the payload length.
        /// </summary>
        public byte PayloadLengthSignature { get; set; }

        /// <summary>
        /// Gets or sets the payload extended length (0 if length is less than 126).
        /// </summary>
        public ulong PayloadExtendedLength { get; set; }

        /// <summary>
        /// Gets the header length (depends from mask and payload length).
        /// </summary>
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

        /// <summary>
        /// Gets the payload length.
        /// </summary>
        public ulong PayloadLength => PayloadLengthSignature < 126 ? PayloadLengthSignature : PayloadExtendedLength;

        /// <summary>
        /// Gets the frame length (sum of header and payload length).
        /// </summary>
        public ulong FrameLength => HeaderLength + PayloadLength;

        /// <summary>
        /// Gets or sets the masking key.
        /// </summary>
        public byte[] MaskingKey { get; set; }

        /// <summary>
        /// Gets or sets the payload (if mask is enabled, message must be decrypted by
        /// <see cref="GetMessage"/> method.
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketFrame"/> class.
        /// </summary>
        /// <param name="mask">The flag indicates whether the mask is used to encrypt payload.</param>
        public WebSocketFrame(bool mask)
        {
            Mask = mask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketFrame"/> class.
        /// </summary>
        /// <param name="data">The payload.</param>
        /// <param name="mask">The flag indicates whether the mask is used to encrypt payload.</param>
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

        /// <summary>
        /// Decrypts payload using the masking key if <see cref="Mask"/> is set.
        /// </summary>
        /// <returns>The decrypted payload.</returns>
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
