using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    /// <summary>
    /// Represents an interface for clients manager.
    /// </summary>
    public interface IWebSocketClientsManager
    {
        /// <summary>
        /// Adds the client to the internal list.
        /// </summary>
        /// <param name="client">The client to add.</param>
        void Add(WebSocketClient client);

        /// <summary>
        /// Removes the client from the internal list.
        /// </summary>
        /// <param name="client">The client to remove.</param>
        void Remove(WebSocketClient client);

        /// <summary>
        /// Gets the client with the specified ID.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>The client with the specified ID or null if cannot be found.</returns>
        WebSocketClient GetById(string id);

        /// <summary>
        /// Gets the client with the specified socket.
        /// </summary>
        /// <param name="socket">The client socket.</param>
        /// <returns>The client with the specified ID or null if cannot be found.</returns>
        WebSocketClient GetBySocket(Socket socket);

        /// <summary>
        /// Checks if there is a client with the specified ID in the internal list.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>True if a client with the specified ID exists, otherwise false.</returns>
        bool Exists(string id);
    }
}
