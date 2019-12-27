using FOIKnjiznica.Classes;
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
    public partial class PrekidRezervacijePopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PrekidRezervacijePopupPage(PovijestPublikacije rezerviranaPublikacija)
        {
            InitializeComponent();

            TekstPrekidaKorisniku.Text = rezerviranaPublikacija.nazivPublikacije;
        }

        private async void NazadPritisnuto(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}