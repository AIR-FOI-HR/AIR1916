using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrekidRezervacijePopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        StanjePublikacije rezervacija;
        public PrekidRezervacijePopupPage(StanjePublikacije rezerviranaPublikacija)
        {
            InitializeComponent();

            TekstPrekidaKorisniku.Text = rezerviranaPublikacija.nazivPublikacije;

            rezervacija = rezerviranaPublikacija;
        }

        private async void NazadPritisnuto(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void PotvrdiPritisnuto(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(rezervacija);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "PrekidRezervacije", content);

            await httpClient.GetAsync("https://foiknjiznicaazurefunkcije.azurewebsites.net/api/OslobodenjeKnjigeNotifikacija?code=FE9IUHyiPZcyzcca7DEzTlBRVWh5HUElZIeLX4UE6JA73cPdoTyP9A==&idPublikacije=" + rezervacija.nazivPublikacije);

            PosaljiObavijest(rezervacija.kopijaId);

            await PopupNavigation.Instance.PopAsync();
        }

        public void PosaljiObavijest(int idKopije)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Classes.Clanovi.hrEduPersonUniqueID);
            mail.To.Add("sdrvoderi@foi.hr");
            mail.Subject = "Prekid rezervacije";
            mail.Body = $"Korisnik {Classes.Clanovi.hrEduPersonUniqueID} je upravo prekinuo rezervaciju knjige sa identifikacijskim brojem {idKopije.ToString()}";

            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fknjiznica@gmail.com", "admin123!");

            SmtpServer.Send(mail);
        }
    }
}