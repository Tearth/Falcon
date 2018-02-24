namespace Falcon.Protocol.Frame
{
    public class FramesManager : IFramesManager
    {
        FrameSerializer _serializer;
        FrameDeserializer _deserializer;

        public FramesManager()
        {
            _serializer = new FrameSerializer();
            _deserializer = new FrameDeserializer();
        }

        public byte[] Serialize(byte[] data, FrameType type)
        {
            var frame = new WebSocketFrame(data, false)
            {
                OpCode = (byte)type,
                FIN = true
            };

            return _serializer.GetBytes(frame);
        }

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
