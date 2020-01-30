using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using InterfaceModule;
using PINModul;
using UzorakModul;
using OtisakModul;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostavkePrijaveModulom : ContentPage
    {
        ClientAuth trenutanNacin;
        IPrijava stariNacinPrijave;
        IPrijava noviNacinPrijave;
        bool potvrdaStarihPodataka;
        public PostavkePrijaveModulom()
        {
            InitializeComponent();
            DohvatiAktivanNacinPrijave();
        }

        private void PinPostavljanje(object sender, EventArgs e)
        {
            stariNacinPrijave.PrijavaModulom(OtvoriStranicuModula, ZatvoriStranicuModula, trenutanNacin.podaci);

            potvrdaStarihPodataka = stariNacinPrijave.StanjeZadnjePrijave;

            if(potvrdaStarihPodataka == true)
            {
                noviNacinPrijave = ImplementiraniModuli.popisModula["4"];
                noviNacinPrijave.PromjenaPodataka(OtvoriStranicuModulaSPotvrdom, ZatvoriStranicuModula, trenutanNacin.podaci);
                PohraniPinUBazu(noviNacinPrijave.UneseniPodatak, 4);
            }
        }


        public async void OtvoriStranicuModula(Type tipUI, Action<Type> zatvaranjeUI, string hashiraniPodatak)
        {
            var args = new object[] { hashiraniPodatak, zatvaranjeUI };
            await Navigation.PushAsync((Page)Activator.CreateInstance(tipUI, args));
        }

        public async void OtvoriStranicuModulaSPotvrdom(Type tipUI, Action<Type> zatvaranjeUI, string hashiraniPodatak, Action<string> vratiPodatke)
        {
            var args = new object[] { hashiraniPodatak, zatvaranjeUI, vratiPodatke };
            await Navigation.PushAsync((Page)Activator.CreateInstance(tipUI, args));
        }

        public void ZatvoriStranicuModula(Type tipUI)
        {
            if (tipUI == null)
            {
            }
            else
            {
                Navigation.PopAsync();
            }
        }

        public async void DohvatiAktivanNacinPrijave()
        {
            trenutanNacin = new ClientAuth();
            List<ClientAuth> nacinPrijave;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "DodajAuthProtocol/" + Clanovi.id);
            if (response.Length > 20)
            {
                var publikacije = JsonConvert.DeserializeObject<List<ClientAuth>>(response);
                nacinPrijave = publikacije;
                trenutanNacin = nacinPrijave.First();
                stariNacinPrijave = ImplementiraniModuli.popisModula[trenutanNacin.Auth_ProtocolId.ToString()];
            }

            client.Dispose();
        }

        private void UzorakPostavljanje(object sender, EventArgs e)
        {

        }

        private void OtisakPostavljanje(object sender, EventArgs e)
        {

        }

        private async void PohraniPinUBazu(string odabraniPin,int idModula)
        {
            string pinHash = odabraniPin;
            HttpClient httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(new ClanoviAuthProtokol() { ClanoviId = Clanovi.id, Auth_ProtocolId = idModula, podaci = pinHash });
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "DodajAuthProtocol/", content);
            App.Current.MainPage = new Profil(1);
        }
    }
}