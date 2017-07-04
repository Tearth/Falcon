using Falcon.Clients;
using Falcon.SocketServices;
using Falcon.SocketServices.EventArguments;
using Falcon.WebSocketEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Falcon
{
    class ServerListener
    {
        Socket socket;
        Task loop;

        ClientsManager clientsManager;
        NewConnectionHandler newConnectionHandler;
        ReceiveDataHandler receiveDataHandler;
        SendDataHandler sendDataHandler;

        static ManualResetEvent loopEvent;

        public event EventHandler<WebSocketNewConnectionArgs> OnWebSocketNewConnection;
        public event EventHandler<WebSocketReceivedDataArgs> OnWebSocketDataReceived;
        public event EventHandler<WebSocketSentDataArgs> OnWebSocketDataSent;
        public event EventHandler<WebSocketDisconnectArgs> OnWebSocketDisconnect;
        public bool Shutdown { get; private set; }

        public ServerListener()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            loop = new Task(Loop);
            loopEvent = new ManualResetEvent(false);

            clientsManager = new ClientsManager();
            newConnectionHandler = new NewConnectionHandler();
            receiveDataHandler = new ReceiveDataHandler();
            sendDataHandler = new SendDataHandler();

            newConnectionHandler.OnNewClientConnection += OnNewClientConnection;
            receiveDataHandler.OnDataReceived += OnNewDataReceived;
            sendDataHandler.OnDataSent += OnDataSent;

            Shutdown = true;
        }

        public void StartListening(IPEndPoint endPoint)
        {
            socket.Bind(endPoint);
            socket.Listen(128);

            Shutdown = false;
            loop.Start();
        }

        public void StopListening()
        {
            Shutdown = true;
            socket.Close();
        }

        void Loop()
        {
            while(!Shutdown)
            {
                loopEvent.Reset();
                newConnectionHandler.BeginConnection(socket);
                loopEvent.WaitOne();
            }    
        }

        void OnNewClientConnection(object sender, NewConnectionArgs args)
        {
            var client = args.Client;

            clientsManager.Add(client);
            receiveDataHandler.ReceiveData(client);

            var connectionArgs = new WebSocketNewConnectionArgs(client.ID);
            OnWebSocketNewConnection(this, connectionArgs);
        }

        private void OnNewDataReceived(object sender, ReceivedDataArgs args)
        {
            var client = args.Client;
            var receivedData = client.Buffer.Take(args.BytesReceived).ToArray();

            var receivedDataArgs = new WebSocketReceivedDataArgs(client.ID, receivedData);
            OnWebSocketDataReceived(this, receivedDataArgs);
        }

        private void OnDataSent(object sender, SentDataArgs args)
        {
            var client = args.Client;

            var sentDataArgs = new WebSocketSentDataArgs(client.ID, args.BytesSent);
            OnWebSocketDataSent(this, sentDataArgs);
        }
    }
}
