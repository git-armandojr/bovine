using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bovine
{
    public static class Constants
    {
        // URL of REST service
        public static string RestUrl = Device.RuntimePlatform == Device.Android ? "http://ec10-ftt.ddns.net:1026/v2/entities{0}" : "http://192.168.1.7:1026/v2/entities{0}";

        public const string ListenConnectionString = "Endpoint=sb://bovine.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=OpiBAmo4srNkEuHIDsY+j7zLo/XbgkvA18sx6bLu8lA=";

        public const string NotificationHubName = "bovine";


    }
}
