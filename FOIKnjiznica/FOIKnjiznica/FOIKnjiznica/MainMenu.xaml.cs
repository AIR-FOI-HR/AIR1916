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
    public partial class MainMenu : MasterDetailPage
    {
        public MainMenu()
        {
            //Skrivanje Navigation Bar-a sa početnog zaslona "Main Menu" koji je ujedno i root navigacije
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            

            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        //Listanje Literature za Pregled na zaslonu
        public void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}