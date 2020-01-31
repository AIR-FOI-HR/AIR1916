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

        }
    }
}