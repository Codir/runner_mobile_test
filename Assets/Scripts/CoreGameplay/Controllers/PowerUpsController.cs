using System;
using System.Collections.Generic;
using System.Linq;
using CoreGameplay.Models;

namespace CoreGameplay.Controllers
{
    //Class with logics of manipulate powerUps for each hero
    public class PowerUpsController
    {
        public event Action OnActiveListUpdatedEvent;

        private readonly List<CollectibleEntityModel> _powerUpsEffects;
        private readonly List<CollectibleEntityModel> _powerUpsForRemove;

        public PowerUpsController()
        {
            _powerUpsEffects = new List<CollectibleEntityModel>();
            _powerUpsForRemove = new List<CollectibleEntityModel>();
        }

        public void Update(float deltaTime)
        {
            foreach (var effect in _powerUpsEffects)
            {
                effect.TimeLeft -= deltaTime;
                if (effect.TimeLeft <= 0)
                {
                    _powerUpsForRemove.Add(effect);
                }
            }

            RemovePowerUps();
        }

        public BaseEntityModel ApplyToModel(BaseEntityModel entityModel)
        {
            return _powerUpsEffects.Aggregate(entityModel, (current, effect) => (current + effect));
        }

        public void AddItems(List<CollectibleEntityModel> items)
        {
            if (items is not { Count: > 0 }) return;

            foreach (var item in items)
            {
                Add(item);
            }

            OnActiveListUpdatedEvent.SafeInvoke();
        }

        private void Add(CollectibleEntityModel item)
        {
            _powerUpsEffects.Add(item);
        }

        private void RemovePowerUps()
        {
            if (_powerUpsForRemove.Count <= 0) return;

            foreach (var effect in _powerUpsForRemove)
            {
                _powerUpsEffects.Remove(effect);
            }

            OnActiveListUpdatedEvent.SafeInvoke();
        }
    }
}