using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSeed.ViewModel.Core;
using XamarinSeed.ViewModel;
using XamarinSeed.Core;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        IViewLocator _viewLocator;
        INavigationService _navigationService;

        public MainPage(IViewLocator viewLocator, INavigationService navigationService)
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            _viewLocator = viewLocator;
            _navigationService = navigationService;

            this.Master = _viewLocator.Get(Common.Enums.ViewType.Menu, null);
        }

        public async Task NavigateFromMenu(Common.Enums.ViewType viewType)
        {
            await _navigationService.PushPageAsync(viewType, isMasterDetailChildView: true);
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            IsPresented = false;
        }
    }
}