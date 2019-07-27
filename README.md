# Falcon: WebSocket server library for .NET

Falcon is a simple implementation of WebSocket protocol, based on [RFC6455](https://tools.ietf.org/html/rfc6455). The main class of this library is WebSocketServer, which includes:

#### Events
 * **WebSocketConnected** - triggered when a new client has connected
 * **WebSocketDataReceived** - triggered when new data has received
 * **WebSocketDataSent** - triggered when a data has been successfully sent
 * **WebSocketDisconnected** - triggered when the client has disconnected

#### Methods
 * **Start** - starts WebSocket server with the specified port
 * **Stop** - closes WebSocket server
 * **SendData** - sends specified data to the client
 * **SendRawData** - sends raw (without WebSocket frame) data to the client
 * **DisconnectClient** - removes connection with the client
 * **GetClientInfo** - returns information (IP, port, ...) about the client

# Statistics
  * **Source lines of code**: ~1800
  * **Comments**: ~750

# Used libraries
 * Xunit

# Examples
![Example 1](https://i.imgur.com/ECepzwH.png)