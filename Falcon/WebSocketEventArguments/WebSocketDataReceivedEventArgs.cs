using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketEventArguments
{
    class WebSocketDataReceivedEventArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public byte[] Data { get; private set; }

        public WebSocketDataReceivedEventArgs(String clientID, byte[] data)
        {
            this.ClientID = clientID;
            this.Data = data;
        }
    }
}
