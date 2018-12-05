using System;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinSeed.Model.Entity;
using XamarinSeed.ViewModel.Core;

namespace XamarinSeed.ViewModel
{
    public interface IItemDetailViewModel
    {
        Item Item { get; set; }
        ICommand LoadCommand { get; }
    }

    public class ItemDetailViewModel : BaseViewModel, IItemDetailViewModel
    {
        public Item Item { get; set; }
        public ICommand LoadCommand { get; private set; }

        public ItemDetailViewModel()
        {
            this.LoadCommand = new Command<Item>((obj) => OnLoadCommandExecuted(obj));
        }

        private void OnLoadCommandExecuted(Item obj)
        {
            Item = obj;
        }
    }
}
