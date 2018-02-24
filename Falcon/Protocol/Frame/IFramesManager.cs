namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents an interface for serialize and deserialize methods.
    /// </summary>
    public interface IFramesManager
    {
        /// <summary>
        /// Serializes payload to WebSocket frame with the specified type.
        /// </summary>
        /// <param name="data">The payload to encapsulate.</param>
        /// <param name="type">The WebSocket frame type.</param>
        /// <returns>The serialized WebSocket frame.</returns>
        byte[] Serialize(byte[] data, FrameType type);

        /// <summary>
        /// Deserializes WebSocket frame into plain data (payload).
        /// </summary>
        /// <param name="data">The array of bytes with WebSocket frame.</param>
        /// <param name="result">The deserialization result.</param>
        /// <param name="type">The WebSocket frame type.</param>
        /// <param name="parsedBytes">The parsed bytes count.</param>
        /// <returns>The deserialized frame payload.</returns>
        byte[] Deserialize(byte[] data, out DeserializationResult result, out FrameType type, out int parsedBytes);
    }
}
