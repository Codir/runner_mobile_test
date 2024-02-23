using CoreGameplay.Hero;
using DLib.UI;
using Events;
using UI.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoreGameplay
{
    public class CoreGameplayController
    {
        #region fields

        #endregion

        #region fields

        private readonly HeroController _heroController;
        private readonly LevelController _levelController;

        private int _passedDistance;
        private Vector3 _tapOffset;
        private bool _isTapped;
        private bool _isLevelLoaded;

        #endregion

        #region constructor

        public CoreGameplayController(HeroController heroController, Transform levelContainer)
        {
            _heroController = heroController;
            _heroController.Init();
            _levelController = new LevelController(levelContainer);
        }

        #endregion

        #region public methods

        public void LoadLevel()
        {
            if (_isLevelLoaded)
            {
                UnloadLevel();
            }

            _heroController.OnLoadLevel();
            _levelController.OnLoadLevel();

            _isLevelLoaded = true;
        }

        public void UnloadLevel()
        {
            if (!_isLevelLoaded) return;

            _heroController.OnUnloadLevel();
            _levelController.OnUnloadLevel();
            _passedDistance = 0;

            _isLevelLoaded = false;
        }

        public void Update(float deltaTime)
        {
            if (_levelController.CheckLevelFail(_heroController.transform.position))
            {
                UIScreensManager.ChangeScreen(UIScreenTypes.FailScreen,
                    new FailScreenModelView() { Distance = _passedDistance });
            }

            _levelController.CheckHeroPosition(_heroController.transform.position);

            if (_isTapped)
            {
                _heroController.OnMouseMove(Input.mousePosition - _tapOffset);
                _tapOffset = Input.mousePosition;
            }
            else
            {
                _heroController.OnMouseMove(Vector3.zero);
            }

            Inputs();
        }


        public void FixedUpdate(float fixedDeltaTime)
        {
            UpdateDistance();
        }

        #endregion

        #region private methods

        private void Inputs()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                _heroController.OnTap();

                _tapOffset = Input.mousePosition;
                _isTapped = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTapped = false;
            }
        }

        private void UpdateDistance()
        {
            _passedDistance = _heroController.GetPassedDistance();
            AppController.EventBus.Invoke(new UpdateDistanceEvent(_passedDistance));
        }

        #endregion
    }
}