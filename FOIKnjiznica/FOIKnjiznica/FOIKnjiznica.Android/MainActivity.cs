﻿using System;

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


namespace FOIKnjiznica.Droid
{
    [Activity(Label = "FOIKnjiznica", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        WebView webView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

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
            LoadApplication(new App());
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