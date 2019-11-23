﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuMaster : ContentPage
    {
        public ListView ListView;

        public MainMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MainMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuMasterMenuItem> MenuItems { get; set; }

            public MainMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainMenuMasterMenuItem>(new[]
                {
                    new MainMenuMasterMenuItem { Id = 0, Title = "Moj profil" },
                    new MainMenuMasterMenuItem { Id = 1, Title = "Pretraži knjižnicu" },
                    new MainMenuMasterMenuItem { Id = 2, Title = "Favoriti" },
                    new MainMenuMasterMenuItem { Id = 3, Title = "Postavke" },
                    new MainMenuMasterMenuItem { Id = 4, Title = "Slanje poruke" },
                    new MainMenuMasterMenuItem { Id = 5, Title = "Odjava" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}