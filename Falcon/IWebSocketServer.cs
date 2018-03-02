using System;
using System.Net;
using Falcon.Exceptions;
using Falcon.Protocol.Frame;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;

namespace Falcon
{
    /// <summary>
    /// Represents an interface to manage WebSocket server.
    /// </summary>
    public interface IWebSocketServer
    {
        /// <summary>
        /// Gets the buffer size for each client.
        /// </summary>
        uint BufferSize { get; }

        /// <summary>
        /// Gets the current server state.
        /// </summary>
        EServerState ServerState { get; }

        /// <summary>
        /// Event triggered when a new WebSocket client has been connected.
        /// </summary>
        event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;

        /// <summary>
        /// Event triggered when a new data (valid WebSocket frame) has been received.
        /// </summary>
        event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;

        /// <summary>
        /// Event triggered when the specified data has been successfully sent.
        /// </summary>
        event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;

        /// <summary>
        /// Event triggered when a WebSocket client has disconnected.
        /// </summary>
        event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        /// <summary>
        /// Starts the WebSocket server with the specified parameters.
        /// </summary>
        /// <param name="address">The WebSocket server address.</param>
        /// <param name="port">The WebSocket server port.</param>
        /// <exception cref="ServerAlreadyWorkingException">Thrown when the server is already open.</exception>
        void Start(IPAddress address, ushort port);

        /// <summary>
        /// Stops the WebSocket server.
        /// </summary>
        /// <exception cref="ServerAlreadyClosedException">Thrown when the server is already closed.</exception>
        void Stop();

        /// <summary>
        /// Sends a data to the client with the specified id.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <param name="data">The data to send.</param>
        /// <exception cref="ServerAlreadyClosedException">Thrown when the server is already closed.</exception>
        /// <returns>True if the client ID has been found and data has been sent, otherwise false.</returns>
        bool SendData(string clientID, byte[] data);

        /// <summary>
        /// Sends a data to the client with the specified id.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <param name="text">The text to send.</param>
        /// <exception cref="ServerAlreadyClosedException">Thrown when the server is already closed.</exception>
        /// <returns>True if the client ID has been found and data has been sent, otherwise false.</returns>
        bool SendData(string clientID, string text);

        /// <summary>
        /// Sends a data to the client with the specified id.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <param name="data">The data to send.</param>
        /// <param name="type">The WebSocket frame type.</param>
        /// <exception cref="ServerAlreadyClosedException">Thrown when the server is already closed.</exception>
        /// <returns>True if the client ID has been found and data has been sent, otherwise false.</returns>
        bool SendData(string clientID, byte[] data, FrameType type);

        /// <summary>
        /// Sends a raw data (without packing to WebSocket frame) to the client with the specified ID.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <param name="data">The data to send.</param>
        /// <exception cref="ServerAlreadyClosedException">Thrown when the server is already closed.</exception>
        /// <returns>True if the client ID has been found and data has been sent, otherwise false.</returns>
        bool SendRawData(string clientID, byte[] data);

        /// <summary>
        /// Gets the client information.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <returns>The client information if the specified ID exists, otherwise null.</returns>
        ClientInfo GetClientInfo(string clientID);

        /// <summary>
        /// Disconnects a client with the specified ID.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        void DisconnectClient(string clientID);
    }
}
