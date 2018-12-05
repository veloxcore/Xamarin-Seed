using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinSeed.Common;
using XamarinSeed.Core;
using XamarinSeed.ViewModel.Core;

namespace XamarinSeed
{
    public class StartupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ViewLocator>().As<IViewLocator>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<CacheHelper>().As<Common.ICacheHelper>();
            builder.RegisterType<ConnectionManager>().As<Common.IConnectionManager>();
            
            // Register all views
            RegisterViews(builder);

            ViewModel.Bootstrap.RegisterDependency(builder);
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<Views.MainPage>().Keyed<Page>(Enums.ViewType.MainPage).Named<object>("parameter").SingleInstance();
            builder.RegisterType<Views.MenuPage>().Keyed<Page>(Enums.ViewType.Menu).Named<object>("parameter");
            builder.RegisterType<Views.NewItemPage>().Keyed<Page>(Enums.ViewType.NewItem).Named<object>("parameter");
            builder.RegisterType<Views.ItemsPage>().Keyed<Page>(Enums.ViewType.Items).Named<object>("parameter");
            builder.RegisterType<Views.ItemDetailPage>().Keyed<Page>(Enums.ViewType.ItemDetails).Named<object>("parameter");
            builder.RegisterType<Views.AboutPage>().Keyed<Page>(Enums.ViewType.About).Named<object>("parameter");
        }
    }
}
