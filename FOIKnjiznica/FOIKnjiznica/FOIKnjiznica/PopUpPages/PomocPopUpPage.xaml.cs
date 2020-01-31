using FOIKnjiznica.Classes;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PomocPopUpPage : PopupPage
    {
        public PomocPopUpPage(string pomocNaslov, string pomocOpis)
        {
            Pomoc.TextPomoc = pomocNaslov;
            lblPomoc.Text = Pomoc.TextPomoc;
            Pomoc.TextPomocOpis = pomocOpis;
            lblPomocOpis.Text = Pomoc.TextPomocOpis;
            InitializeComponent();
        }

        private async void izlazak_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }


}