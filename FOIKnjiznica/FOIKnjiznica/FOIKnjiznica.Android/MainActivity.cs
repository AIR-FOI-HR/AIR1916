using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;

using System.Net;
using Android.Webkit;
using Android.Net.Http;

using Lottie.Forms.Droid;
using Rg.Plugins.Popup.Services;

using Android.Util;
using Android.Gms.Common;
using Poz1.NFCForms.Droid;
using Android.Nfc;
using Android.Content;
using Poz1.NFCForms.Abstract;

namespace FOIKnjiznica.Droid
{
    [Activity(Label = "FOIKnjiznica", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        WebView webView;
        public const string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        public NfcAdapter NFCdevice;
        public NfcForms x;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            IsPlayServicesAvailable();
            CreateNotificationChannel();

            //Inicijalizacija POPUP nugget paketa
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //Inicijalizacija Android Lottie nugget paketa
            AnimationViewRenderer.Init();
            CachedImageRenderer.Init(true);
            /*
            SetContentView(Resource.Layout.WebFOIPrijava);
            if (true)
            {
                webView = FindViewById<WebView>(Resource.Id.prijavawebview);
                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
                webView.Settings.DomStorageEnabled = true;
                webView.Settings.UseWideViewPort = true;
                webView.Settings.LoadWithOverviewMode = true;

                webView.SetWebViewClient(new HelloWebViewClient());
                webView.LoadUrl("https://192.168.0.110:45455/");

            }*/

            //Implementacija NFC-a za skeniranje publikacije
            NfcManager NfcManager = (NfcManager)Application.Context.GetSystemService(Context.NfcService);
            NFCdevice = NfcManager.DefaultAdapter;

            Xamarin.Forms.DependencyService.Register<INfcForms,NfcForms>();
            x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;

            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (NFCdevice != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                NFCdevice.EnableForegroundDispatch
                (
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter (NfcAdapter.ActionTechDiscovered) },
                    new String[][] { new string[]
                    {
                        NFCTechs.Ndef
                    },
                    new string[]
                    {
                        NFCTechs.MifareClassic
                    },
                    }
                );
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            NFCdevice.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            x.OnNewIntent(this, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        // Služi za zatvaranje popup ekrana kod pritiska gumba za nazad na mobilnome uređaju
        public async override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug(TAG, "This device is not supported");
                    Finish();
                }
                return false;
            }

            Log.Debug(TAG, "Google Play Services is available.");
            return true;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = CHANNEL_ID;
            var channelDescription = string.Empty;
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }

    public class HelloWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, IWebResourceRequest request)
        {
            view.LoadUrl(request.Url.ToString());
            return false;
        }

        public override void OnReceivedSslError(Android.Webkit.WebView view, SslErrorHandler handler, SslError error)
        {
            handler.Proceed();
        }
    }
}