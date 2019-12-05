﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using FOIKnjiznicaWebServis.Models;
using FOIKnjiznicaWebServis.Controllers;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuDetail : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        public MainMenuDetail()
        {
            InitializeComponent();
            BindingContext = this;
            DohvatiPublikacije();

            //Listener koji prima događaj od popup prozora te osvjezava listu
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeAZ", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeZA", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjePoGodini", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjePoAutoru", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "filtriranjePublikacija", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "resetiranjeFiltera", (sender) => { OsvjeziListuPublikacija(); });
        }

        //Dohvacanje Publikacije za prikaz na zaslonu
        private async void DohvatiPublikacije()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Publikacije");
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = publikacije;
        }

        private async void UnosPretrazivanja(object sender, TextChangedEventArgs e)
        {
            string id = search_bar.Text.ToString();
            id = e.NewTextValue;
            if (id.Length > 0)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/PretragaPublikacija/"+id);
                var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                ListaPublikacije.ItemsSource = publikacije;
            }
            else
            {
                DohvatiPublikacije();
            }
        }

        private async void filter_button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new IzbornikFiltracije());
        }
        
        private void OsvjeziListuPublikacija()
        {
            ListaPublikacije.ItemsSource = Classes.Filtar.FiltrirajPublikacije(listaSvihPublikacija);
        }

        private async void sort_button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.SortiranjePopupPage());
        }

        public async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Classes.Publikacije tappedItem = e.Item as Classes.Publikacije;
            await Navigation.PushAsync(new BookInfo(tappedItem));
        }
    }
}