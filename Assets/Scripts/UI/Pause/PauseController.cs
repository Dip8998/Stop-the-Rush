using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace STR.UI
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnResumeButtonClicked()
        {
            Time.timeScale = 1f; 
            gameObject.SetActive(false); 
            UIService.Instance.ShowGameplayPanel(true);
        }

        private void OnRestartButtonClicked()
        {
            UIService.Instance.RestartGame();   
        }

        private void OnMainMenuButtonClicked()
        {
            UIService.SkipMainMenuOnce = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Active (bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
