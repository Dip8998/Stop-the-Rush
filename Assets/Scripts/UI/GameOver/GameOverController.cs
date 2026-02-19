using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace STR.UI
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnRestartButtonClicked()
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
    }
}
