using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XamarinSeed.ViewModel.Mediator
{
    internal class WeakAction : WeakReference
    {
        readonly MethodInfo _method;

        public object Subscriber { get; }

        internal WeakAction(Action<object> action, object subscriber)
            : base(action.Target)
        {
            if (subscriber == null)
                throw new ArgumentNullException("Subscriber");

            _method = action.GetMethodInfo();
            Subscriber = subscriber;
        }

        internal Action<object> CreateAction()
        {
            if (!base.IsAlive)
                return null;

            try
            {
                // Rehydrate into a real Action
                // object, so that the method
                // can be invoked on the target.

                return _method.CreateDelegate(typeof(Action<object>),
                    base.Target)
                    as Action<object>;
            }
            catch
            {
                return null;
            }
        }
    }
}
