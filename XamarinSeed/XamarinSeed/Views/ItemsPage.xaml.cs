using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinSeed.ViewModel;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        IItemsViewModel viewModel;

        public ItemsPage(IItemsViewModel itemsViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = itemsViewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Model.Entity.Item;
            if (item == null)
                return;

            await viewModel.LoadDetailsPage(item);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}