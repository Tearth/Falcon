using Falcon.SocketClients;
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
        ConnectingService newConnectionService;
        ReceivingDataService receiveDataService;
        SendingDataService sendDataService;

        int bufferSize;
        static ManualResetEvent loopEvent;

        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;
        public bool Shutdown { get; private set; }

        public ServerListener(int bufferSize)
        {
            this.bufferSize = bufferSize;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            loop = new Task(Loop);
            loopEvent = new ManualResetEvent(false);

            clientsManager = new ClientsManager();
            newConnectionService = new ConnectingService();
            receiveDataService = new ReceivingDataService();
            sendDataService = new SendingDataService();

            newConnectionService.Connected += OnConnected;
            newConnectionService.Disconnected += OnDisconnected;

            receiveDataService.ReceivedData += OnReceivedData;
            receiveDataService.Disconnected += OnDisconnected;

            sendDataService.SentData += OnSentData;
            sendDataService.Disconnected += OnDisconnected;

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

        public void SendData(String clientID, byte[] data)
        {
            var client = clientsManager.Get(clientID);
            sendDataService.SendData(client, data);
        }

        void Loop()
        {
            while(!Shutdown)
            {
                loopEvent.Reset();
                newConnectionService.BeginConnection(socket);
                loopEvent.WaitOne();
            }    
        }

        void OnConnected(object sender, ConnectedEventArgs args)
        {
            var client = new Client(args.ClientSocket, bufferSize);

            clientsManager.Add(client);
            receiveDataService.ReceiveData(client);

            var connectionArgs = new WebSocketConnectedEventArgs(client.ID);
            WebSocketConnected(this, connectionArgs);

            loopEvent.Set();
        }

        void OnReceivedData(object sender, DataReceivedEventArgs args)
        {
            var client = args.Client;
            var receivedData = client.Buffer.Take(args.BytesReceived).ToArray();

            var receivedDataArgs = new WebSocketDataReceivedEventArgs(client.ID, receivedData);
            WebSocketDataReceived(this, receivedDataArgs);

            receiveDataService.ReceiveData(client);
        }

        void OnSentData(object sender, DataSentEventArgs args)
        {
            var sentDataArgs = new WebSocketDataSentEventArgs(args.Client.ID, args.BytesSent);
            WebSocketDataSent(this, sentDataArgs);
        }

        void OnDisconnected(object sender, DisconnectedEventArgs args)
        {
            clientsManager.Remove(args.Client);

            var disconnectArgs = new WebSocketDisconnectedEventArgs(args.Client.ID, args.Exception);
            WebSocketDisconnected(this, disconnectArgs);
        }
    }
}
