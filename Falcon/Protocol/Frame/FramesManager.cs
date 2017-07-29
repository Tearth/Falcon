namespace Falcon.Protocol.Frame
{
    internal class FramesManager
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
            var frame = new WebSocketFrame(data, false);
            frame.OpCode = (byte)type;
            frame.FIN = true;

            return _serializer.GetBytes(frame);
        }

        public byte[] Deserialize(byte[] data, out DeserializeResult result, out FrameType type, out int parsedBytes)
        {
            var frame = _deserializer.GetFrame(data, out result);
            if (result != DeserializeResult.SuccessWithFIN && result != DeserializeResult.SuccessWithoutFIN)
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
