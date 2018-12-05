using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinSeed.Model.Entity;
using XamarinSeed.ViewModel.Core;

namespace XamarinSeed.ViewModel
{
    public interface INewItemViewModel
    {
        Item Item { get; set; }

        ICommand SaveCommand { get; }
        ICommand CancelCommand { get; }
    }

    public class NewItemViewModel : BaseViewModel, INewItemViewModel
    {
        INavigationService _navigationService;

        private Item _item;
        public Item Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public NewItemViewModel(INavigationService navigationService, Mediator.IMediator mediator)
            : base(mediator)
        {
            _navigationService = navigationService;

            Item = new Item
            {
                Id = 999,
                Title = "Item name",
                Completed = false
            };

            this.SaveCommand = new Command(async () => await OnSaveCommand());
            this.CancelCommand = new Command(async () => await OnCancelCommand());
        }

        private Task OnCancelCommand()
        {
            return _navigationService.PopPageAsync();
        }

        private Task OnSaveCommand()
        {
            Mediator.NotifyColleagues(Common.Enums.MediatorMessageType.AddItem, Item);
            return _navigationService.PopPageAsync();
        }
    }
}
