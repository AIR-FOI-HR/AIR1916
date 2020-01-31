using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Odjava : ContentPage
	{
		public Odjava ()
		{
			InitializeComponent ();
            OdjaviKorisnika();
		}

        private async Task OdjaviKorisnika()
        {
            var httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(new Clan() { id=Clanovi.id, hrEduPersonUniqueID=Clanovi.hrEduPersonUniqueID, mobitelID = Clanovi.mobitelID });
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "Clanovi/", content);
            System.Environment.Exit(0);
        }
	}
}