﻿using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDisconnectedEventArgs : EventArgs
    {
        public string ClientID { get; }
        public bool Unexpected => Exception != null;
        public Exception Exception { get; }

        public WebSocketDisconnectedEventArgs(string clientID)
        {
            ClientID = clientID;
        }

        public WebSocketDisconnectedEventArgs(string clientID, Exception exception) : this(clientID)
        {
            Exception = exception;
        }
    }
}
