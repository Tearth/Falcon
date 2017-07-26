using Falcon;
using Falcon.WebSocketEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    class Core
    {
        WebSocketServer webSocketServer;

        public Core()
        {
            webSocketServer = new WebSocketServer();

            webSocketServer.WebSocketConnected += WebSocketServer_Connected;
            webSocketServer.WebSocketDataReceived += WebSocketServer_DataReceived;
            webSocketServer.WebSocketDataSent += WebSocketServer_DataSent;
            webSocketServer.WebSocketDisconnected += WebSocketServer_Disconnected;
        }

        private void WebSocketServer_Connected(object sender, WebSocketConnectedEventArgs e)
        {
            var clientInfo = webSocketServer.GetClientInfo(e.ClientID);

            Console.WriteLine("New client connected!");
            Console.WriteLine("  ID: " + e.ClientID);
            Console.WriteLine("  IP: " + clientInfo.IP);
            Console.WriteLine("  Port: " + clientInfo.Port);
        }

        private void WebSocketServer_DataReceived(object sender, WebSocketDataReceivedEventArgs e)
        {
            Console.Write("Received: ");
            Console.WriteLine(ASCIIEncoding.UTF8.GetString(e.Data));

            foreach (byte b in e.Data)
                Console.Write(b + " ");
            Console.WriteLine();
        }

        private void WebSocketServer_DataSent(object sender, WebSocketDataSentEventArgs e)
        {
            Console.WriteLine("Sent " + e.SentBytes + " bytes");
        }

        private void WebSocketServer_Disconnected(object sender, WebSocketDisconnectedEventArgs e)
        {
            Console.WriteLine("Client disconnected. ID: " + e.ClientID);
            Console.WriteLine("Unexpected: " + e.Unexpected);

            if (e.Unexpected)
                Console.WriteLine(e.Exception.ToString());
        }

        public void Run()
        {
            Console.WriteLine("Starting WebSocket server...");
            webSocketServer.Start(IPAddress.Any, 7444);

            Console.Read();
        }
    }
}
