using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSeed.ViewModel.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class MasterMenuItem
    {
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public Common.Enums.ViewType View { get; set; }
    }
}
