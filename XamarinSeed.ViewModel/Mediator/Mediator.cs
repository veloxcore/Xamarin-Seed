using System;
using System.Collections.Generic;
using System.Text;
using XamarinSeed.Common;

namespace XamarinSeed.ViewModel.Mediator
{
    public interface IMediator
    {
        void Register(Enums.MediatorMessageType messageType, Action<object> callback);
        void NotifyColleagues(Enums.MediatorMessageType messageType, object parameter);
        void Unregister(object subscriber);
    }

    public class Mediator : IMediator
    {
        readonly MessageToActionsMap _messageToCallbacksMap;
        
        public Mediator()
        {
            _messageToCallbacksMap = new MessageToActionsMap();
        }

        #region "Public Methods"

        public void NotifyColleagues(Enums.MediatorMessageType message, object parameter)
        {
            List<Action<object>> actions = _messageToCallbacksMap.GetActions(message);

            if (actions != null)
            {
                foreach (Action<object> action in actions)
                {
                    action(parameter);
                }
            }
        }

        public void Register(Enums.MediatorMessageType message, Action<object> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            if (callback.Target == null)
                throw new ArgumentException("The 'callback' delegate must reference an instance method.");

            _messageToCallbacksMap.AddAction(message, callback);
        }

        public void Unregister(object subscriber)
        {
            _messageToCallbacksMap.RemoveActions(subscriber);
        }
        #endregion
    }
}
