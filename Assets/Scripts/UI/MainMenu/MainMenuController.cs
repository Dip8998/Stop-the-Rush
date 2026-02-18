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
            Time.timeScale = 0f;
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            Active(false);
            UIService.Instance.ShowGameplayPanel(true);
            Time.timeScale = 1f;
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
