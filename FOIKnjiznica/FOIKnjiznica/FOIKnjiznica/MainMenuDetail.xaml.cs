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

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuDetail : ContentPage
    {
        public MainMenuDetail()
        {
            InitializeComponent();
            BindingContext = this;
            DohvatiPublikacije();
        }

        //Dohvacanje Publikacije za prikaz na zaslonu
        private async void DohvatiPublikacije()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Publikacije");
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            ListaPublikacije.ItemsSource = publikacije;
        }
    }
}