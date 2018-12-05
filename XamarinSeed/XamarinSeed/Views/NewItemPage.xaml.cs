using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSeed.Model.Entity;
using XamarinSeed.ViewModel;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage(INewItemViewModel newItemViewModel)
        {
            InitializeComponent();
            
            BindingContext = newItemViewModel;
        }
    }
}