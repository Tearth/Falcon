namespace Falcon.Protocol.Frame
{
    public interface IFramesManager
    {
        byte[] Serialize(byte[] data, FrameType type);
        byte[] Deserialize(byte[] data, out DeserializeResult result, out FrameType type, out int parsedBytes);
    }
}
