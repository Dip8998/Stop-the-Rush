using UnityEngine;
using UnityEngine.UI;

namespace STR.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            UIEvents.RaisePlayRequested();
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        public void Active(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
