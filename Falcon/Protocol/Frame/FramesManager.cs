namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents a set of methods to serialize and deserialize frames.
    /// </summary>
    public class FramesManager : IFramesManager
    {
        private FrameSerializer _serializer;
        private FrameDeserializer _deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FramesManager"/> class.
        /// </summary>
        public FramesManager()
        {
            _serializer = new FrameSerializer();
            _deserializer = new FrameDeserializer();
        }

        /// <inheritdoc/>
        public byte[] Serialize(byte[] data, FrameType type)
        {
            var frame = new WebSocketFrame(data, false)
            {
                OpCode = (byte)type,
                FIN = true
            };

            return _serializer.GetBytes(frame);
        }

        /// <inheritdoc/>
        public byte[] Deserialize(byte[] data, out DeserializationResult result, out FrameType type, out int parsedBytes)
        {
            var frame = _deserializer.GetFrame(data, out result);
            if (result != DeserializationResult.SuccessWithFIN && result != DeserializationResult.SuccessWithoutFIN)
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
