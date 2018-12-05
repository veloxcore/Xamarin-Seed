using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinSeed.Common;
using XamarinSeed.Model.Entity;
using XamarinSeed.Service;
using XamarinSeed.Service.Data;
using XamarinSeed.ViewModel.Core;
using XamarinSeed.ViewModel.Mediator;

namespace XamarinSeed.ViewModel
{
    public interface IItemsViewModel
    {
        SmartCollection<Item> Items { get; set; }
        ICommand LoadItemsCommand { get; }
        ICommand AddItemCommand { get; }
        Task LoadDetailsPage(Item item);
    }
    public class ItemsViewModel : BaseViewModel, IItemsViewModel
    {
        #region Private Fields
        INavigationService _navigationService;
        ISampleService _sampleService;
        IUserDialogs _userDialogs;
        #endregion

        private SmartCollection<Item> _items = new SmartCollection<Item>();
        public SmartCollection<Item> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ICommand LoadItemsCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }

        public ItemsViewModel(ISampleService sampleService, INavigationService navigationService, IMediator mediator)
            : base(mediator)
        {
            Title = "Items";
            _sampleService = sampleService;
            _navigationService = navigationService;
            this._userDialogs = UserDialogs.Instance;

            this.LoadItemsCommand = new Command(async () => await OnLoadItemsCommandExecute());
            this.AddItemCommand = new Command(async () => await OnAddItemCommandExecute());

            mediator.Register(Enums.MediatorMessageType.AddItem, OnAddItemComplete);
        }

        private Task OnAddItemCommandExecute()
        {
            return _navigationService.PushPageAsync(Enums.ViewType.NewItem);
        }

        private void OnAddItemComplete(object obj)
        {
            var newItem = obj as Item;
            Items.Add(newItem);
        }

        private async Task OnLoadItemsCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                await _sampleService.GetAndFetchSampleDataAsync(NotifyItems);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void NotifyItems(ServiceResponse<List<Item>> obj)
        {
            if(obj.Status == Common.Enums.ResponseStatus.Success)
            {
                Items.Reset(obj.Data);
            }
            else
            {
                _userDialogs.HideLoading();
                obj.HandleFailure();
            }
        }

        public Task LoadDetailsPage(Item item)
        {
            return _navigationService.PushPageAsync(Enums.ViewType.ItemDetails, data : item);
        }
    }
}
