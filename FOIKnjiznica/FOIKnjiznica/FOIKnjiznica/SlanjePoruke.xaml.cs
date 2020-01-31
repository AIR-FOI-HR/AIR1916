using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Mail;
using Plugin.Toast;
using Rg.Plugins.Popup.Services;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlanjePoruke : ContentPage
    {
        public SlanjePoruke()
        {
            InitializeComponent();
        }

        private async void Posalji(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Classes.Clanovi.hrEduPersonUniqueID);
            mail.To.Add("sdrvoderi@foi.hr");
            mail.Subject = NaslovPoruke.Text;
            mail.Body = TijeloPoruke.Text + "\r\n Poslao: " + Classes.Clanovi.hrEduPersonUniqueID;

            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fknjiznica@gmail.com", "admin123!");

            SmtpServer.Send(mail);

            NaslovPoruke.Text = "";
            TijeloPoruke.Text = "";

            CrossToastPopUp.Current.ShowCustomToast($"Poruka je poslana!", "#ae2323", "White");
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA KONTAKTIRANJE KNJIŽNICE!", "Prikazan Vam je mini obrazac za slanje poruke. Potrebno je dodati naslov poruke te detaljan sadržaj iste u kojoj se opisuje svrha poruke. Klikom na gumb pošalji, knjižnica je kontaktirana."));
        }

    }
}