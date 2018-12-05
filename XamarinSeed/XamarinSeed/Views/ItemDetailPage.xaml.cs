using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinSeed.ViewModel;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        IItemDetailViewModel viewModel;

        public ItemDetailPage(IItemDetailViewModel viewModel, object parameter)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            viewModel.LoadCommand.Execute(parameter);
        }
    }
}