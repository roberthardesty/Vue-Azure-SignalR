using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.pi.Services
{
    public class ProgressService : ServiceClient
    {
        public ProgressService(): base(HubNames.ProgressHub)
        {

        }

    }
}
