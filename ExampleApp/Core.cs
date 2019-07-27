using System;
using System.Net;
using System.Text;
using Falcon;
using Falcon.WebSocketEventArguments;

namespace ExampleApp
{
    public class Core
    {
        readonly WebSocketServer _webSocketServer;

        public Core()
        {
            _webSocketServer = new WebSocketServer(8192);

            _webSocketServer.WebSocketConnected += WebSocketServer_Connected;
            _webSocketServer.WebSocketDataReceived += WebSocketServer_DataReceived;
            _webSocketServer.WebSocketDataSent += WebSocketServer_DataSent;
            _webSocketServer.WebSocketDisconnected += WebSocketServer_Disconnected;
        }

        private void WebSocketServer_Connected(object sender, WebSocketConnectedEventArgs e)
        {
            var clientInfo = _webSocketServer.GetClientInfo(e.ClientId);

            Console.WriteLine("New client connected!");
            Console.WriteLine("  ID: " + e.ClientId);
            Console.WriteLine("  IP: " + clientInfo.Ip);
            Console.WriteLine("  Port: " + clientInfo.Port);
        }

        private void WebSocketServer_DataReceived(object sender, WebSocketDataReceivedEventArgs e)
        {
            Console.Write("Received: ");
            Console.WriteLine(Encoding.UTF8.GetString(e.Data));

            foreach (var b in e.Data)
            {
                Console.Write(b + " ");
            }

            Console.WriteLine();

            _webSocketServer.SendData(e.ClientId, e.Data);
        }

        private void WebSocketServer_DataSent(object sender, WebSocketDataSentEventArgs e)
        {
            Console.WriteLine("Sent " + e.SentBytes + " bytes");
        }

        private void WebSocketServer_Disconnected(object sender, WebSocketDisconnectedEventArgs e)
        {
            Console.WriteLine("Client disconnected. ID: " + e.ClientId);
            Console.WriteLine("Unexpected: " + e.Unexpected);

            if (e.Unexpected)
            {
                Console.WriteLine(e.Exception.ToString());
            }
        }

        public void Run()
        {
            Console.WriteLine("Starting WebSocket server...");
            _webSocketServer.Start(IPAddress.Any, 7444);

            Console.Read();
        }
    }
}
