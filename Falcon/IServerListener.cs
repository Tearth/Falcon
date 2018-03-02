using System;
using System.Net;
using System.Net.Sockets;
using Falcon.SocketServices.EventArguments;

namespace Falcon
{
    /// <summary>
    /// Represents an interface for server listener.
    /// </summary>
    public interface IServerListener
    {
        /// <summary>
        /// The event triggered when a client socket has connected to the WebSocket server.
        /// </summary>
        event EventHandler<ConnectedEventArgs> ClientConnected;

        /// <summary>
        /// The event triggered when new data has been received.
        /// </summary>
        event EventHandler<DataReceivedEventArgs> DataReceived;

        /// <summary>
        /// The event triggered when a data has been sent to the client.
        /// </summary>
        event EventHandler<DataSentEventArgs> DataSent;

        /// <summary>
        /// The event triggered when a client socket has been closed.
        /// </summary>
        event EventHandler<DisconnectedEventArgs> ClientDisconnected;

        /// <summary>
        /// Gets the current server state.
        /// </summary>
        EServerState ServerState { get; }

        /// <summary>
        /// Starts listening and accepts incoming client connections.
        /// </summary>
        /// <param name="endpoint">The server endpoint.</param>
        void Start(IPEndPoint endpoint);

        /// <summary>
        /// Stops listening and shutdowns the server socket.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sends data to the specified client socket.
        /// </summary>
        /// <param name="socket">The client socket which will receive the specified data.</param>
        /// <param name="data">The data to send.</param>
        void Send(Socket socket, byte[] data);

        /// <summary>
        /// Closes connection with the specified socket.
        /// </summary>
        /// <param name="socket">The client socket to close.</param>
        void CloseConnection(Socket socket);

        /// <summary>
        /// Closes connection with the specified socket due to some exception.
        /// </summary>
        /// <param name="socket">The client socket to close.</param>
        /// <param name="ex">The exception.</param>
        void CloseConnection(Socket socket, Exception ex);
    }
}
