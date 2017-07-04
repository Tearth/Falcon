using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketHandlers
{
    class ClientData
    {
        public String ClientID { get; set; }
        public byte[] Data { get; set; }

        public ClientData()
        {
            ClientID = String.Empty;
        }
    }
}
