# Falcon: WebSocket server library for .NET
Version 1.0

Falcon is a simple implementation of WebSocket protocol, based on [RFC6455](https://tools.ietf.org/html/rfc6455). Main class of this library is WebSocketServer, which includes:

**Events:**
 * WebSocketConnected - triggered when a new client has connected
 * WebSocketDataReceived - triggered when new data has received
 * WebSocketDataSent - triggered when data has been successfully sent
 * WebSocketDisconnected - triggered when client has disconnected
**Methods:**
 * Open - starts WebSocket server with the specified port
 * Close - closes WebSocket server
 * SendData - sends specified data to the client
 * SendRawData - sends raw (without WebSocket frame) to the client
 * DisconnectClient - removes connection with client

Library supports only insecure ws protocol. Support for secure wss will be added later.