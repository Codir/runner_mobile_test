using System;
using System.Collections.Generic;
using DLib.AttributesSystem;
using DLib.EventBus;
using Events;

namespace CoreGameplay.Hero
{
    public struct CollectibleModel
    {
        public BaseAttribute[] Attributes;
        public float TimeLeft;
    }

    public class ActiveEffectsController : IEventListener<CollectGameEvent>
    {
        #region fields

        private readonly Action<List<CollectibleModel>> _onActiveListUpdated;
        private readonly List<CollectibleModel> _activeEffects;

        #endregion

        #region contructor

        public ActiveEffectsController(Action<List<CollectibleModel>> onActiveListUpdated)
        {
            _onActiveListUpdated = onActiveListUpdated;

            _activeEffects = new List<CollectibleModel>();

            AppController.EventBus.Subscribe(this);
        }


        ~ActiveEffectsController()
        {
            AppController.EventBus.Unsubscribe(this);
        }

        #endregion

        #region public methods

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _activeEffects.Count; i++)
            {
                var effect = _activeEffects[i];
                effect.TimeLeft -= deltaTime;
                _activeEffects[i] = effect;
            }

            if (_activeEffects.RemoveAll(item => item.TimeLeft <= 0) > 0)
            {
                _onActiveListUpdated.SafeInvoke(_activeEffects);
            }
        }

        public void OnEvent(CollectGameEvent gameEvent)
        {
            var properties = new BaseAttribute[gameEvent.Item.PropertyItems.Length];
            for (int i = 0; i < gameEvent.Item.PropertyItems.Length; i++)
            {
                properties[i] = gameEvent.Item.PropertyItems[i].GetProperty();
            }

            Add(new CollectibleModel()
            {
                Attributes = properties,
                TimeLeft = gameEvent.Item.TimeLeft
            });
        }

        public void Clear()
        {
            _activeEffects.Clear();
        }

        #endregion

        #region private methods

        private void Add(CollectibleModel item)
        {
            _activeEffects.Add(item);

            _onActiveListUpdated.SafeInvoke(_activeEffects);
        }

        #endregion
    }
}