using FOIKnjiznica.Classes;
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

            TekstPrekidaKorisniku.Text = "Jeste li sigurni da želite prekinuti rezervaciju publikacije "+rezerviranaPublikacija.nazivPublikacije;
        }
    }
}