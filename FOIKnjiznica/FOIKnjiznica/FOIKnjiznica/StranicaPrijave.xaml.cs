using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InterfaceModule;
using PINModul;
using UzorakModul;
using OtisakModul;
using System.Net.Http;
using Newtonsoft.Json;
using FOIKnjiznica.Classes;
using Plugin.DeviceInfo;

namespace FOIKnjiznica
{
    public class Clan
    {
        public int id { get; set; }
        public string hrEduPersonUniqueID { get; set; }
        public string mobitelID { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StranicaPrijave : ContentPage
    {
        public IPrijava prijavaModularno;
        Clan trenutniClan;
        ClientAuthProtocol trenutanNacin;
        public string info;

        public StranicaPrijave()
        {
            InitializeComponent();

            DohvatiKorisnika();       
        }

        private void PokreniPrijavu()
        {
            prijavaModularno = ImplementiraniModuli.popisModula[trenutanNacin.Auth_ProtocolId.ToString()];

            prijavaModularno.PrijavaModulom(OtvoriStranicuModula, ZatvoriStranicuModula, trenutanNacin.podaci);
        }

        public async void OtvoriStranicuModula(Type tipUI,Action<Type> zatvaranjeUI,string hashiraniPodatak)
        {
           var args = new object[] {hashiraniPodatak,zatvaranjeUI};
           await Navigation.PushAsync((Page)Activator.CreateInstance(tipUI, args));
        }

        public void ZatvoriStranicuModula(Type tipUI)
        {
            if(tipUI == null)
            {
                KreirajKorisnika();
                UdiUAplikaciju();
            }
            else 
            {
                KreirajKorisnika();
                Navigation.PopAsync();
                UdiUAplikaciju();
            }
        }

        private void KreirajKorisnika()
        {
            Clanovi.hrEduPersonUniqueID = trenutniClan.hrEduPersonUniqueID;
            Clanovi.id = trenutniClan.id;
            Clanovi.mobitelID = trenutniClan.mobitelID;
        }

        public void UdiUAplikaciju()
        {
            if (prijavaModularno.StanjeZadnjePrijave == true)
            {
                Navigation.PushAsync(new MainMenu());
            }
        }

        public async void DohvatiKorisnika()
        {
            trenutniClan = new Clan();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "PrijavaTrenutniClan/" + CrossDeviceInfo.Current.Id);
            if (response.Length > 20) 
            {
                var publikacije = JsonConvert.DeserializeObject<List<Clan>>(response);
                foreach (var clan in publikacije)
                {
                    trenutniClan.id = clan.id;
                    trenutniClan.hrEduPersonUniqueID = clan.hrEduPersonUniqueID;
                    trenutniClan.mobitelID = clan.mobitelID;
                }
                DohvatiAktivanNacinPrijave();
            }
            client.Dispose();
        }
        public async void DohvatiAktivanNacinPrijave()
        {
            trenutanNacin = new ClientAuthProtocol();
            List<ClientAuthProtocol> nacinPrijave;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "DodajAuthProtocol/" + trenutniClan.id);
            if (response.Length > 20)
            {
                var publikacije = JsonConvert.DeserializeObject<List<ClientAuthProtocol>>(response);
                nacinPrijave = publikacije;
                trenutanNacin = nacinPrijave.First();
                PokreniPrijavu();
            }

            client.Dispose();
        }
    }
}