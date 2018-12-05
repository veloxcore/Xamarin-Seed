using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;

namespace XamarinSeed.ViewModel.Core
{
    public interface INavigationService
    {
        Task PopToRootAsync(bool animate = true);

        Task PushPageAsync(Enums.ViewType page, bool modal = false, bool animate = true, object data = null, bool isMasterDetailChildView = false);

        Task PopPageAsync(bool modal = false, bool animate = true);
    }
}
