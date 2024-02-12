using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MenuScreenView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            ScreensManager.ChangeScreen(ScreenTypes.GameplayScreen);
        }
    }
}