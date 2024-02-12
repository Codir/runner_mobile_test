using UI;
using UI.Models;
using UI.Views;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    public enum MovementTypes
    {
        Run,
        Fly
    }

    //Class with lLogic of core gameplay
    public class CoreGameplayController : MonoBehaviour
    {
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private Vector3 _heroSpawnPosition;

        public static CoreGameplayController Instance;

        public HeroController HeroController { get; private set; }
        public CameraController CameraController { get; private set; }
        public LevelController LevelController { get; private set; }

        private bool _isActive;
        private GameplayScreenModelView _gameplayScreenModelView;

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            CameraController = new CameraController();
            LevelController = new LevelController();
            HeroController = new HeroController();

            ScreensManager.OnChangeScreenEvent += OnChangeScreen;
        }

        private void OnChangeScreen(ScreenTypes type)
        {
            if (type == ScreenTypes.GameplayScreen)
            {
                Init();
                _gameplayScreenModelView = ScreensManager.CurrentScreen?.View
                    .GetComponent<BaseScreenView<GameplayScreenModelView>>()?.Model;
            }
            else
            {
                _gameplayScreenModelView = null;
                _isActive = false;
            }
        }

        private void Init()
        {
            HeroController.Set(_heroSpawnPosition, _levelContainer);

            CameraController.SetTarget(HeroController.View.transform);
            CameraController.Reset();

            LevelController.Init(HeroController.View.transform, _levelContainer);

            _isActive = true;
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;

            HeroController.FixedUpdate();
            OnUpdateDistance();
        }

        private void OnUpdateDistance()
        {
            var distance = HeroController.View.transform.position.x - _heroSpawnPosition.x;
            if (_gameplayScreenModelView != null)
            {
                _gameplayScreenModelView.Distance = Mathf.FloorToInt(distance);
            }
        }

        void Update()
        {
            if (!_isActive) return;

            LevelController.Update(Time.deltaTime);

            if (HeroController != null)
            {
                HeroController.CheckIsGrounded(Time.deltaTime);


                HeroController.Update(Time.deltaTime);
            }

            CameraController.Update(Time.deltaTime);
        }
    }
}