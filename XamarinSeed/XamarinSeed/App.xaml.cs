using Autofac;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSeed.Common;
using XamarinSeed.Core;
using XamarinSeed.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinSeed
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            IContainer container = RegisterDependency();
            var _viewLocator = container.Resolve<IViewLocator>();

            MainPage = _viewLocator.Get(Enums.ViewType.MainPage, null);
        }

        private IContainer RegisterDependency()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<StartupModule>();
            var container = builder.Build();
            return container;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
