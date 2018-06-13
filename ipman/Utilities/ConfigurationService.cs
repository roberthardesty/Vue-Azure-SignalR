using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Utilities
{
    public static class ConfigurationService
    {
        public static IConfiguration Configuration { get; set; }
    }
}
