namespace Falcon.Protocol.Frame
{
    public interface IFramesManager
    {
        byte[] Serialize(byte[] data, FrameType type);
        byte[] Deserialize(byte[] data, out DeserializationResult result, out FrameType type, out int parsedBytes);
    }
}
