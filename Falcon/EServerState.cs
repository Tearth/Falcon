using System.Diagnostics.CodeAnalysis;

namespace Falcon
{
    /// <summary>
    /// Represents the WebSocket server states.
    /// </summary>
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public enum EServerState
    {
        None,
        Working,
        Closed
    }
}
