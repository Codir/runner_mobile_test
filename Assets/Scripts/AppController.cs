using CoreGameplay;
using CoreGameplay.Hero;
using DLib.EventBus;
using DLib.UI;
using UnityEngine;

public class AppController : MonoBehaviour
{
    #region serialize fields

    [SerializeField] private Transform LevelContainer;
    [SerializeField] private CameraController Camera;
    [SerializeField] private HeroController HeroPrefab;

    #endregion

    #region fields

    public static EventBus EventBus { get; private set; }

    private CoreGameplayController _coreGameplayController;

    private UIScreenTypes _cashedCurrentUIScreenType;

    #endregion

    #region engine methods

    private void Awake()
    {
        EventBus = new EventBus();
    }

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (_cashedCurrentUIScreenType == UIScreenTypes.GameplayScreen)
        {
            _coreGameplayController.FixedUpdate(Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (_cashedCurrentUIScreenType == UIScreenTypes.GameplayScreen)
        {
            _coreGameplayController.Update(Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        UIScreensManager.OnChangeScreenEvent -= OnChangeScreen;
    }

    #endregion

    #region private methods

    private void Init()
    {
        UIScreensManager.OnChangeScreenEvent += OnChangeScreen;

        var hero = InitHero();

        _coreGameplayController = new CoreGameplayController(hero, LevelContainer);
    }

    private void OnChangeScreen(UIScreenTypes type)
    {
        _cashedCurrentUIScreenType = type;

        if (_cashedCurrentUIScreenType == UIScreenTypes.GameplayScreen)
        {
            _coreGameplayController.LoadLevel();
            Camera.Reset();
        }
        else
        {
            _coreGameplayController.UnloadLevel();
        }
    }

    private HeroController InitHero()
    {
        var hero = Instantiate(HeroPrefab, LevelContainer);
        Camera.SetTarget(hero.transform);
        hero.OnUnloadLevel();

        return hero;
    }

    #endregion
}