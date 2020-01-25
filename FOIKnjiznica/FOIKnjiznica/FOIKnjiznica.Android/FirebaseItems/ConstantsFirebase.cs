using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FOIKnjiznica.Droid.FirebaseItems
{
    public static class ConstantsFirebase
    {
        public const string ListenConnectionString = "Endpoint=sb://foiknjiznicanotificationhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=umd3NlQlz23/d85XntiSOEL7+Z0aXfpOdDPLLZCZCwQ=";
        public const string NotificationHubName = "FoiKnjiznicaNotificationHub";
    }
}