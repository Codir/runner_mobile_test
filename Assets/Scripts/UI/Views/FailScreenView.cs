using System.Globalization;
using DLib.UI;
using TMPro;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class FailScreenView : UIBaseScreenView<FailScreenModelView>
    {
        #region serialize fields

        [SerializeField] private TextMeshProUGUI DistanceTextLabel;
        [SerializeField] private Button MenuButton;
        [SerializeField] private Button RestartButton;

        #endregion

        #region fields

        private string _distanceTextFormat;

        #endregion

        #region engine methods

        private void Awake()
        {
            _distanceTextFormat = DistanceTextLabel.text;
        }

        private void OnEnable()
        {
            OnDistanceChanged(Model.Distance);

            MenuButton.onClick.AddListener(OnMenuButtonClick);
            RestartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDisable()
        {
            MenuButton.onClick.RemoveListener(OnMenuButtonClick);
            RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        #endregion

        #region private methods

        private void OnDistanceChanged(float value)
        {
            DistanceTextLabel.text = string.IsNullOrEmpty(_distanceTextFormat)
                ? value.ToString(CultureInfo.InvariantCulture)
                : string.Format(_distanceTextFormat, value);
        }

        private void OnMenuButtonClick()
        {
            UIScreensManager.ChangeScreen(UIScreenTypes.MainMenuScreen);
        }

        private void OnRestartButtonClick()
        {
            UIScreensManager.ChangeScreen(UIScreenTypes.GameplayScreen);
        }

        #endregion
    }
}