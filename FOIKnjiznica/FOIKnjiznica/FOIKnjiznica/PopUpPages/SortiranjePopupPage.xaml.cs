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
	public partial class SortiranjePopupPage : PopupPage
	{
        private List<Classes.Publikacije> publikacijeZaSortiranje;
		public SortiranjePopupPage ()
		{
			InitializeComponent();
            publikacijeZaSortiranje = MainMenuDetail.listaSvihPublikacija;
        }

        /// <summary>
        /// Funkcija kojom se uzima lista svih publikacija te se unutar nje sortira po abecednom redu uzlazno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sortirajAZ_Clicked(object sender, EventArgs e)
        {
            var sortiranaLista = publikacijeZaSortiranje.OrderBy(publikacija => publikacija.naziv);
            MainMenuDetail.listaSvihPublikacija = sortiranaLista.ToList();
            
            //Slanje roditelju događaja
            MessagingCenter.Send<App>((App)Application.Current, "sortiranjeAZ");
            await PopupNavigation.Instance.PopAsync(); //Zatvaranje popup prozora
        }

        private async void izlazak_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// Funkcija kojom se uzima lista svih publikacija te se unutar nje sortira po abecednom redu silazno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sortirajZA_Clicked(object sender, EventArgs e)
        {
            var sortiranaLista = publikacijeZaSortiranje.OrderByDescending(publikacija => publikacija.naziv);
            MainMenuDetail.listaSvihPublikacija = sortiranaLista.ToList();

            MessagingCenter.Send<App>((App)Application.Current, "sortiranjeZA");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void sortirajPoGodinama_Clicked(object sender, EventArgs e)
        {
            var sortiranaLista = publikacijeZaSortiranje.OrderBy(publikacija => publikacija.godina_izdanja);
            MainMenuDetail.listaSvihPublikacija = sortiranaLista.ToList();

            MessagingCenter.Send<App>((App)Application.Current, "sortiranjePoGodini");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void sortirajPoAutorima_Clicked(object sender, EventArgs e)
        {
            var sortiranaLista = publikacijeZaSortiranje.OrderBy(publikacija => publikacija.Autor);
            MainMenuDetail.listaSvihPublikacija = sortiranaLista.ToList();

            MessagingCenter.Send<App>((App)Application.Current, "sortiranjePoAutoru");
            await PopupNavigation.Instance.PopAsync();
        }
    }
}