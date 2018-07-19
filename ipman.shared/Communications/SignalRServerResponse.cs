using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Communications
{
    public class SignalRServerResponse
    {
        public bool Success { get; set; }
        public Exception Error { get; set; }
    }
}
