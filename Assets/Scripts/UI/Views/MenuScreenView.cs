using DLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MenuScreenView : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Button PlayButton;

        #endregion

        #region engine methods

        private void OnEnable()
        {
            PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            PlayButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        #endregion

        #region private methods

        private void OnPlayButtonClick()
        {
            UIScreensManager.ChangeScreen(UIScreenTypes.GameplayScreen);
        }

        #endregion
    }
}