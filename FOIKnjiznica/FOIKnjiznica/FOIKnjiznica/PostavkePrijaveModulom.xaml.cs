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
        int idModula;
        IPrijava noviNacinPrijave;
        public PostavkePrijaveModulom()
        {
            InitializeComponent();
        }

        private void PinPostavljanje(object sender, EventArgs e)
        {
            noviNacinPrijave = ImplementiraniModuli.popisModula["4"];

            idModula = 4;

            noviNacinPrijave.PromjenaPodataka(OtvoriStranicuModulaSPotvrdom, ZatvoriStranicuModulaSPotvrdom, null);

        }

        private void UzorakPostavljanje(object sender, EventArgs e)
        {
            noviNacinPrijave = ImplementiraniModuli.popisModula["2"];

            idModula = 2;

            noviNacinPrijave.PromjenaPodataka(OtvoriStranicuModulaSPotvrdom, ZatvoriStranicuModulaSPotvrdom, null);
        }

        private void OtisakPostavljanje(object sender, EventArgs e)
        {
            noviNacinPrijave = ImplementiraniModuli.popisModula["3"];

            idModula = 3;

            noviNacinPrijave.PromjenaPodataka(OtvoriStranicuModulaSPotvrdom, ZatvoriStranicuModulaSPotvrdom, null);
        }

        public async void OtvoriStranicuModulaSPotvrdom(Type tipUI, Action<Type> zatvaranjeUI, string hashiraniPodatak, Action<string> vratiPodatke)
        {
            var args = new object[] { hashiraniPodatak, zatvaranjeUI, vratiPodatke };
            await Navigation.PushAsync((Page)Activator.CreateInstance(tipUI, args));
        }

        public void ZatvoriStranicuModulaSPotvrdom(Type tipUI)
        {
            if (tipUI == null)
            {
                PohraniPinUBazu(noviNacinPrijave.UneseniPodatak, idModula);
            }
            else
            {
                PohraniPinUBazu(noviNacinPrijave.UneseniPodatak, idModula);
                Navigation.PopAsync();
            }
        }

        private async void PohraniPinUBazu(string odabraniPin,int idModula)
        {
            string pinHash = odabraniPin;
            HttpClient httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(new ClanoviAuthProtokol() { ClanoviId = Clanovi.id, Auth_ProtocolId = idModula, podaci = pinHash });
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "DodajAuthProtocol/", content);
        }
    }
}