using System;
using DLib.EventBus;
using DLib.UI.Models;
using Events;

namespace UI.Models
{
    public class GameplayScreenModelView : IUIScreenModel, IEventListener<UpdateDistanceEvent>
    {
        #region fields

        public event Action<int> DistanceChangedEvent;

        private int _distance;

        #endregion

        #region public methods

        public void Subscribe()
        {
            AppController.EventBus.Subscribe(this);
        }

        public void Unsubscribe()
        {
            AppController.EventBus.Unsubscribe(this);
        }

        public void OnEvent(UpdateDistanceEvent @event)
        {
            _distance = @event.Distance;

            DistanceChangedEvent.SafeInvoke(_distance);
        }

        #endregion
    }
}