using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace STR.UI
{
    public class GameWinController : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        public void OnRestartButtonClicked()
        {
            UIService.SkipMainMenuOnce = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        public void Active(bool active)
        {
            gameObject.SetActive(active);
        }

        public void OnMainMenuButtonClicked()
        {
            UIService.SkipMainMenuOnce = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}