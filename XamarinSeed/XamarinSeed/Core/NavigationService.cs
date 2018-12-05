using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSeed.Common;
using XamarinSeed.ViewModel.Core;

namespace XamarinSeed.Core
{
    public class NavigationService : INavigationService
    {
        //public static Stack<Page> NavigationStack = new Stack<Page>();
        IViewLocator _viewLocator;

        private Page CurrentPage
        {
            get
            {
                return ((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).CurrentPage;
            }
        }

        #region Constructor

        public NavigationService(IViewLocator viewLocator)
        {
            _viewLocator = viewLocator;
        }

        #endregion

        #region Public Methods

        public async Task PopPageAsync(bool modal = false, bool animate = true)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (modal)
                {
                    if (CurrentPage.Navigation.ModalStack.Any())
                        await CurrentPage.Navigation.PopModalAsync(animate);
                }
                else
                {
                    if (CurrentPage.Navigation.NavigationStack.Any())
                        await CurrentPage.Navigation.PopAsync(animate);
                }
            });
            await Task.FromResult(0);
        }

        public async Task PopToRootAsync(bool animate = true)
        {
            Device.BeginInvokeOnMainThread(async () => await CurrentPage.Navigation.PopToRootAsync(animate));
            await Task.FromResult(0);
        }

        public async Task PushPageAsync(Enums.ViewType view, bool modal = false, bool animate = true, object data = null, bool isMasterDetailChildView = false)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var page = _viewLocator.Get(view, data);
                
                if (isMasterDetailChildView)
                {
                    var navPage = new NavigationPage(page);
                    MasterDetailPage masterDetailPage = Application.Current.MainPage as MasterDetailPage;
                    masterDetailPage.Detail = navPage;
                    //NavigationService.NavigationStack.Push(page);
                    await Task.FromResult(0);
                }
                else
                {
                    Task t;
                    if (modal)
                    {
                        t = CurrentPage.Navigation.PushModalAsync(page, animate);
                    }
                    else
                        t = CurrentPage.Navigation.PushAsync(page, animate);
                    //NavigationService.NavigationStack.Push(page);
                    await t;
                }
            });

            await Task.FromResult(0);
        }

        #endregion
    }
}
