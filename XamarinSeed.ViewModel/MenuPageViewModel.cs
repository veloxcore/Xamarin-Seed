using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;
using XamarinSeed.ViewModel.Models;

namespace XamarinSeed.ViewModel
{
    public interface IMenuPageViewModel
    {
        SmartCollection<MasterMenuItem> MasterMenuItem { get; set; }
        MasterMenuItem SelectedItem { get; set; }
    }
    public class MenuPageViewModel : BaseViewModel, IMenuPageViewModel
    {
        private SmartCollection<MasterMenuItem> _masterMenuItem = new SmartCollection<MasterMenuItem>();
        public SmartCollection<MasterMenuItem> MasterMenuItem
        {
            get { return _masterMenuItem; }
            set { SetProperty(ref _masterMenuItem, value); }
        }

        private MasterMenuItem _selectedItem;
        public MasterMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public MenuPageViewModel()
        {
            MasterMenuItem.Add(new MasterMenuItem { Title = "Item", ImagePath = "setting.png", View = Enums.ViewType.Items });
            MasterMenuItem.Add(new MasterMenuItem { Title = "About", ImagePath = "setting.png", View = Enums.ViewType.About });

            SelectedItem = MasterMenuItem.First();
        }
    }
}
