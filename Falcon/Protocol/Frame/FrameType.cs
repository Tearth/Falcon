using System.Diagnostics.CodeAnalysis;

namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents WebSocket frame types.
    /// </summary>
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public enum FrameType : byte
    {
        None = 0x00,
        Message = 0x01,
        Disconnect = 0x8,
        Ping = 0x9,
        Pong = 0xA
    }
}
