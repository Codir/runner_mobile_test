using System;
using System.Collections.Generic;
using Configs;
using CoreGameplay.Hero.Systems;
using DLib;
using DLib.AttributesSystem;
using Events;
using UnityEngine;

namespace CoreGameplay.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroController : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Animator Animator;
        [SerializeField] private Rigidbody2D Rigidbody2D;

        #endregion

        #region fields

        private HeroConfig _config;
        private HeroPhysicsModel _model;

        private ActiveEffectsController _activeEffectsController;
        private HeroPhysicsSystem _heroPhysicsSystem;
        private HeroAnimationSystem _heroAnimationSystem;

        #endregion

        #region engine methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            var collectible = other.GetComponent<CollectibleItem>();
            if (collectible == null) return;

            AppController.EventBus.Invoke(new CollectGameEvent(collectible));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ((_config.GroundLayer.value & (1 << collision.gameObject.layer)) == 0) return;

            _model.UsedJumps = 0;
        }

        private void FixedUpdate()
        {
            _heroPhysicsSystem.FixedUpdate(_model, Time.fixedDeltaTime);
            _activeEffectsController.Update(Time.fixedDeltaTime);
        }

        #endregion

        #region public methods

        public void Init()
        {
            _config = ConfigLoader.GetConfig<HeroConfig>();
            _model = new HeroPhysicsModel
            {
                AttributesState = new AttributesStateModel(_config.PropertyItems),
                Rigidbody2D = Rigidbody2D
            };
            _heroAnimationSystem = new HeroAnimationSystem(Animator, _config.HeroAnimationSpeed);
            _heroPhysicsSystem = new HeroPhysicsSystem(_heroAnimationSystem);
            _activeEffectsController = new ActiveEffectsController(OnCollectiblesListWasUpdated);
        }

        public void OnLoadLevel()
        {
            _model.AttributesState = new AttributesStateModel(_config.PropertyItems);
            transform.position = _config.HeroSpawnPosition;

            gameObject.SetActive(true);
        }

        public void OnUnloadLevel()
        {
            gameObject.SetActive(false);
            _activeEffectsController?.Clear();
        }

        public void OnTap()
        {
            _heroPhysicsSystem.OnTap(_model);
        }

        public void OnMouseMove(Vector3 value)
        {
            _model.MouseMove = value;
        }

        public int GetPassedDistance()
        {
            return Mathf.FloorToInt(transform.position.x - _config.HeroSpawnPosition.x);
        }

        #endregion

        #region private methods

        private void OnCollectiblesListWasUpdated(List<CollectibleModel> activeEffects)
        {
            _model.AttributesState.CopyProperties(_config.PropertyItems);
            activeEffects.ForEach(item => _model.AttributesState.Apply(item));
        }

        #endregion
    }
}