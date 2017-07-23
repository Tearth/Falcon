using Falcon.SocketClients;
using Falcon.SocketServices;
using Falcon.SocketServices.ClientInformations;
using Falcon.SocketServices.EventArguments;
using Falcon.WebSocketEventArguments;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Falcon
{
    class ServerListener
    {
        Socket _socket;
        Task _loop;

        ClientsManager _clientsManager;
        ConnectingService _newConnectionService;
        ReceivingDataService _receiveDataService;
        SendingDataService _sendDataService;
        ClientInfoGenerator _clientInfoGenerator;

        int _bufferSize;
        static ManualResetEvent _loopEvent;

        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;
        public bool Shutdown { get; private set; }

        public ServerListener(int bufferSize)
        {
            _bufferSize = bufferSize;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _loop = new Task(Loop);

            _loopEvent = new ManualResetEvent(false);

            _clientsManager = new ClientsManager();
            _newConnectionService = new ConnectingService();
            _receiveDataService = new ReceivingDataService();
            _sendDataService = new SendingDataService();
            _clientInfoGenerator = new ClientInfoGenerator();

            _newConnectionService.Connected += OnConnected;
            _newConnectionService.Disconnected += OnDisconnected;

            _receiveDataService.ReceivedData += OnReceivedData;
            _receiveDataService.Disconnected += OnDisconnected;

            _sendDataService.SentData += OnSentData;
            _sendDataService.Disconnected += OnDisconnected;

            Shutdown = true;
        }

        public void StartListening(IPEndPoint endPoint)
        {
            _socket.Bind(endPoint);
            _socket.Listen(128);

            Shutdown = false;
            _loop.Start();
        }

        public void StopListening()
        {
            Shutdown = true;
            _socket.Close();
        }

        public void SendData(string clientID, byte[] data)
        {
            var client = _clientsManager.Get(clientID);
            _sendDataService.SendData(client, data);
        }

        public void CloseConnection(string clientID)
        {
            CloseConnection(clientID, null);
        }

        public void CloseConnection(string clientID, Exception ex)
        {
            var client = _clientsManager.Get(clientID);
            if (client == null)
                return;

            client.Close();
            _clientsManager.Remove(client);

            OnDisconnected(this, new DisconnectedEventArgs(client, ex));
        }

        public ClientInfo GetClientInfo(string clientID)
        {
            var client = _clientsManager.Get(clientID);
            if (client == null)
                return null;

            return _clientInfoGenerator.Get(client);
        }

        void Loop()
        {
            while(!Shutdown)
            {
                _loopEvent.Reset();
                _newConnectionService.BeginConnection(_socket);
                _loopEvent.WaitOne();
            }    
        }

        void OnConnected(object sender, ConnectedEventArgs args)
        {
            var client = new Client(args.ClientSocket, _bufferSize);
            _clientsManager.Add(client);

            var connectionArgs = new WebSocketConnectedEventArgs(client.ID);
            WebSocketConnected(this, connectionArgs);

            _receiveDataService.ReceiveData(client);
            _loopEvent.Set();
        }

        void OnReceivedData(object sender, DataReceivedEventArgs args)
        {
            var client = args.Client;
            var receivedData = client.Buffer.Take(args.BytesReceived).ToArray();

            var receivedDataArgs = new WebSocketDataReceivedEventArgs(client.ID, receivedData);
            WebSocketDataReceived(this, receivedDataArgs);

            _receiveDataService.ReceiveData(client);
        }

        void OnSentData(object sender, DataSentEventArgs args)
        {
            var sentDataArgs = new WebSocketDataSentEventArgs(args.Client.ID, args.BytesSent);
            WebSocketDataSent(this, sentDataArgs);
        }

        void OnDisconnected(object sender, DisconnectedEventArgs args)
        {
            _clientsManager.Remove(args.Client);

            var disconnectArgs = new WebSocketDisconnectedEventArgs(args.Client.ID, args.Exception);
            WebSocketDisconnected(this, disconnectArgs);
        }
    }
}
