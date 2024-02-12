using System.Globalization;
using TMPro;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameplayScreenView : BaseScreenView<GameplayScreenModelView>
    {
        [SerializeField] private TextMeshProUGUI _distanceTextLabel;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;

        private string _distanceTextFormat;

        private void Awake()
        {
            _distanceTextFormat = _distanceTextLabel.text;
        }

        private void OnEnable()
        {
            Model.DistanceChangedEvent += OnDistanceChanged;

            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDisable()
        {
            Model.DistanceChangedEvent -= OnDistanceChanged;

            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        private void OnDistanceChanged(float value)
        {
            _distanceTextLabel.text = string.IsNullOrEmpty(_distanceTextFormat)
                ? value.ToString(CultureInfo.InvariantCulture)
                : string.Format(_distanceTextFormat, value);
        }

        private void OnMenuButtonClick()
        {
            ScreensManager.ChangeScreen(ScreenTypes.MainMenuScreen);
        }

        private void OnRestartButtonClick()
        {
            ScreensManager.ChangeScreen(ScreenTypes.GameplayScreen);
        }
    }
}