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

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StranicaPrijave : ContentPage
    {
        public IPrijava prijavaModularno;
        public StranicaPrijave()
        {
            InitializeComponent();

            prijavaModularno = new UzorakPrijava();

            prijavaModularno.PrijavaModulom(OtvoriStranicuModula, ZatvoriStranicuModula, "7ZRvZdLHhdkOgnxf/Yec47ScaNTIgBMHQXan5zvFi88=");
        }

        public async void OtvoriStranicuModula(Type tipUI,Action<Type> zatvaranjeUI,string hashiraniPodatak)
        {
           var args = new object[] {hashiraniPodatak,zatvaranjeUI};
           await Navigation.PushAsync((Page)Activator.CreateInstance(tipUI, args));
        }

        public void ZatvoriStranicuModula(Type tipUI)
        {
            Navigation.PopAsync();
        }

        public void UdiUAplikaciju()
        {
            if (prijavaModularno.StanjeZadnjePrijave == true)
            {
                Navigation.PushAsync(new MainMenu());
            }
        }

        public void DohvatiKorisnika()
        {

        }
    }
}