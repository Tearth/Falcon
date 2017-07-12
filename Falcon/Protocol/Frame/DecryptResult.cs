using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    enum DecryptResult
    {
        None,
        InvalidHeader,
        PartialMessage,
        SuccessWithFIN,
        SuccessWithoutFIN
    }
}
