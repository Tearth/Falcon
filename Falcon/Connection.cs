using Falcon.Clients;
using Falcon.ConnectionHandlers;
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
    class Connection
    {
        Socket socket;
        Task loop;

        NewConnectionHandler newConnectionHandler;
        ReceiveDataHandler receiveDataHandler;
        SendDataHandler sendDataHandler;

        static ManualResetEvent loopEvent;

        public bool Shutdown { get; private set; }

        public Connection()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            loop = new Task(Loop);
            loopEvent = new ManualResetEvent(false);

            newConnectionHandler = new NewConnectionHandler();
            receiveDataHandler = new ReceiveDataHandler();
            sendDataHandler = new SendDataHandler();

            newConnectionHandler.OnNewClientConnection += OnNewClientConnection;

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

        void OnNewClientConnection(object sender, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
