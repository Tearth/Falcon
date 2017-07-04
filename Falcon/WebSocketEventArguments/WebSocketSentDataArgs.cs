using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketEventArguments
{
    class WebSocketSentDataArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public int SentBytes { get; private set; }

        public WebSocketSentDataArgs(String clientID, int sentBytes)
        {
            this.ClientID = clientID;
            this.SentBytes = sentBytes;
        }
    }
}
