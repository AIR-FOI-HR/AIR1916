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

using System.Collections.Generic;
using Plugin.DeviceInfo;
using System.Threading.Tasks;
using System.Linq;

using PCLStorage;
using Xamarin.Forms;
using Org.Apache.Http.Util;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FOIKnjiznica.Droid
{
    [Activity(Label = "FOIKnjiznica", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            
            //Inicijalizacija DeviceInfo nuget paketa
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //Inicijalizacija POPUP nuget paketa
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //Inicijalizacija Android Lottie nugget paketa
            AnimationViewRenderer.Init();
            CachedImageRenderer.Init(true);

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
}