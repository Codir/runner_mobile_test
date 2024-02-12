using System;
using UnityEngine;

namespace UI
{
    public enum ScreenTypes
    {
        MainMenuScreen,
        GameplayScreen,
        FailScreen
    }

    [Serializable]
    public class ScreenItem
    {
        public ScreenTypes Type;
        public GameObject View;
    }

    public class ScreensManager : MonoBehaviour
    {
        public static event Action<ScreenTypes> OnChangeScreenEvent;
        public static ScreenItem CurrentScreen { get; private set; }

        [SerializeField] private ScreenTypes _startScreen;
        [SerializeField] private ScreenItem[] _screens;

        private static ScreenItem[] _staticScreensArray;

        private void Awake()
        {
            _staticScreensArray = _screens;

            foreach (var screen in _staticScreensArray)
            {
                screen.View.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            ChangeScreen(_startScreen);
        }

        public static void ChangeScreen(ScreenTypes type)
        {
            foreach (var screen in _staticScreensArray)
            {
                screen.View.gameObject.SetActive(screen.Type == type);
                if (screen.Type == type)
                {
                    CurrentScreen = screen;
                }
            }

            OnChangeScreenEvent.SafeInvoke(type);
        }
    }
}