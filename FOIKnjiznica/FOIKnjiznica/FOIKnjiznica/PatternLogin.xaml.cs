using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FaulandCc.XF.GesturePatternView;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatternLogin : ContentPage
    {
        public PatternLogin()
        {
            InitializeComponent();
        }
        private async void MyGesturePatternView_OnGesturePatternCompleted(object sender, GesturePatternCompletedEventArgs e)
        {
            await DisplayAlert("Gesture", e.GesturePatternValue, "Ok", "Cancel");
            this.MyGesturePatternView.Clear();
        }
    }
}