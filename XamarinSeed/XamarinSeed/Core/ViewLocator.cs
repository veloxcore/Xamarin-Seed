using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinSeed.Common;

namespace XamarinSeed.Core
{
    /// <summary>
    /// Used to locate the view and handaling parameter data
    /// </summary>
    public interface IViewLocator
    {
        Page Get(Enums.ViewType view, object data);
    }
    public class ViewLocator : IViewLocator
    {
        private ILifetimeScope _container;
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewLocator"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public ViewLocator(ILifetimeScope builder)
        {
            _container = builder;
        }

        /// <summary>
        /// Gets the specified page name.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Cannot open view as IBaseView is not implemented for page '" + pageName.ToString()</exception>
        public Page Get(Enums.ViewType pageName, object data)
        {
            return _container.ResolveKeyed<Page>(pageName, new NamedParameter("parameter", data));
        }
    }
}
