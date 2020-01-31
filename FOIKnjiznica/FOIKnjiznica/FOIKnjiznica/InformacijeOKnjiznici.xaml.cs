using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacijeOKnjiznici : ContentPage
    {
        public InformacijeOKnjiznici()
        {
            InitializeComponent();
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA INFORMACIJE O KNJIŽNICI!", "Prikazan Vam je uvid u kontakt i osnovne informacije o knjižnici sa aktivnim radnim vremenom iste. Prikazana je i lokacija na FOI-u radi lakše orjentacije."));
        }
    }
}