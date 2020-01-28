using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Http;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace FOIKnjiznica.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false)]
    public class PokreniAplikacijuActivity : Activity
    {
        Android.Webkit.WebView webView;
        public static string UserID { get; set; }
        public static string MobitelID { get; set; }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Provjeravanje postojanja mobitela te otvaranje logina ukoliko id nije upisan u bazi
            PokreniAplikacijuActivity.MobitelID = CrossDeviceInfo.Current.Id;
            List<Classes.Mobitel> sviMobiteli = await Classes.Clanovi.DohvatiMobiteleSvihClanova();
            CookieManager.Instance.RemoveAllCookie();
            bool postoji = false;
            foreach (var item in sviMobiteli) 
            {
                if (item.MobitelId == PokreniAplikacijuActivity.MobitelID)
                {
                    postoji = true;
                    break;
                }
                else
                {
                    postoji = false;
                }
            }
            if (postoji == true)
            {
                //druga prijava
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (postoji == false)
            {
                //Osiguranje da se bilo koji UI update događa na korektnoj dretvi
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetContentView(Resource.Layout.WebFOIPrijava);
                    webView = FindViewById<Android.Webkit.WebView>(Resource.Id.prijavawebview);
                    webView.Settings.JavaScriptEnabled = true;
                    webView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
                    webView.Settings.DomStorageEnabled = true;
                    webView.Settings.UseWideViewPort = true;
                    webView.Settings.LoadWithOverviewMode = true;

                    webView.SetWebViewClient(new HelloWebViewClient());
                    webView.LoadUrl("https://192.168.0.110:45455/");

                });
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public static async Task UpisiKorisnikaUBazu(string uid, string mid)
        {
            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("IdUsera", uid);
            jsonValues.Add("IdMobitela", mid);
            var httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(jsonValues);
            StringContent stringContent = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync("http://foiknjiznica2.azurewebsites.net/api/FOIAutentikacija/", stringContent);
        }
    }

    /// <summary>
    /// Klasa koja omogućuje otvaranje FOI prijave u Android.WebViewu.
    /// </summary>
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

        /// <summary>
        /// Metoda se poziva više puta prilikom učitavanja našeg WebViewa te će svaki put pokušati izvršiti funkciju "EvaluateJavascript".
        /// Ta funkcija vraća željeni odgovor u trenutku uspješne prijave, u suprotnom ispisuje poruku da ne može pronaći zadanu funkciju "upisiId()"
        /// što je normalno jer funkcija nastaje tek nakon uspješne prijave.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="url"></param>
        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            view.EvaluateJavascript("javascript:upisiId();", new JavaScriptRezultat());
        }

    }

    /// <summary>
    /// Nužna klasa koja omogućuje poziv "EvaluateJavascript" funkcije koja
    /// dohvaća vrijednost putem overrideane funkcije "OnReceiveValue"
    /// </summary>
    public class JavaScriptRezultat : Java.Lang.Object, IValueCallback
    {
        public string Rezultat;
        /// <summary>
        /// Override funkcije koja nam omogućuje čitanje podatka iz JavaScripta
        /// Pošto je riječ o jednostavnom JSON odgovoru, iz rezultata se samo "brišu" '\' kako bi dobili običan string.
        /// </summary>
        /// <param name="value"> JSON odgovor od strane JavaScripta </param>
        public async void OnReceiveValue(Java.Lang.Object value)
        {
            Java.Lang.String jsonOdgovor = (Java.Lang.String)value;
            Rezultat = jsonOdgovor.ToString();
            PokreniAplikacijuActivity.UserID = Rezultat.Replace("\"", string.Empty);
            var provjeraId = PokreniAplikacijuActivity.UserID;
            if (provjeraId != "null")
            {
                var provjeraMobId = PokreniAplikacijuActivity.MobitelID;
                await PokreniAplikacijuActivity.UpisiKorisnikaUBazu(provjeraId, provjeraMobId);
                // "Prisila" webviewa da otvori MainActivity kako bi aplikacija nastavila s radom
                Android.Content.Intent intent = new Android.Content.Intent(Android.App.Application.Context, typeof(MainActivity));
                Android.App.Application.Context.StartActivity(intent);
            }
        }
    }
}