using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.pi.Utilities
{
    public class PiConfiguration
    {
        public static string AzureStorageConnectionString { get; } = @"DefaultEndpointsProtocol=https;AccountName=robfunctionstorage;AccountKey=KrRmLOidXY7C8zMolkzBceJ4HFk2PTgOrMV1ug7s8Pwzyt8S2n6lfIBUwZc1UkLcqdgTT4WXtgsiCT62N2QGAg==;EndpointSuffix=core.windows.net";
        public static string DevSignalRAddress { get; } = @"http://192.168.1.68:5005";
        public static string AzureSignalRAddress { get; } = @"https://ipman-dev.azurewebsites.net/";
        public static string AzureSignalRAccessKey { get; } = @"raggloQ3WDUyhSOIKg6BFqb9wKTryYbYh288YoPLs+s=";
    }
}
