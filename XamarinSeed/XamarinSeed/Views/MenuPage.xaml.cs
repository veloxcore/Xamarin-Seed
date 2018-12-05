using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSeed.ViewModel;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public MenuPage(IMenuPageViewModel menuPageViewModel)
        {
            InitializeComponent();

            this.BindingContext = menuPageViewModel;

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                Common.Enums.ViewType viewType = ((ViewModel.Models.MasterMenuItem)e.SelectedItem).View;
                if (RootPage != null)
                    await RootPage.NavigateFromMenu(viewType);
            };
            //ListViewMenu.SelectedItem = menuPageViewModel.SelectedItem;
        }
    }
}