using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketConnectedEventArgs : EventArgs
    {
        public string ClientID { get; }

        public WebSocketConnectedEventArgs(string clientID)
        {
            ClientID = clientID;
        }
    }
}
