using UnityEngine;

namespace STR.UI
{
    public class UIService : MonoBehaviour
    {
        private static UIService instance;
        public static UIService Instance => instance;

        public static bool SkipMainMenuOnce { get; set; }

        [SerializeField] private GameplayController gameplayController;

        [SerializeField] private MainMenuController mainMenuController;

        [SerializeField] private GameOverController gameOverController;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            Time.timeScale = 0f;
        }

        private void Start()
        {
            if (SkipMainMenuOnce)
            {
                Time.timeScale = 1f;
                SkipMainMenuOnce = false;
                ShowMainMenuPanel(false);
                ShowGameplayPanel(true);
                ShowGameOverPanel(false);
                return;
            }

            ShowMainMenuPanel(true);
            ShowGameplayPanel(false);
            ShowGameOverPanel(false);
        }

        public void UpdateMoney(int money)
        {
            if (gameplayController != null)
            {
                gameplayController.UpdateMoney(money);
            }
        }

        public void UpdateWaveText(int waveNumber)
        {
            if (gameplayController != null)
            {
                gameplayController.UpdateWaveText(waveNumber);
            }
        }

        public void UpdateWaveTimer(float timeRemaining)
        {
            if (gameplayController != null)
            {
                gameplayController.UpdateWaveTimer(timeRemaining);
            }
        }

        public void ShowWaveTimerText(bool show)
        {
            if (gameplayController != null)
            {
                gameplayController.ShowWaveTimerText(show);
            }
        }

        public void ShowGameplayPanel(bool show)
        {
            if (gameplayController != null)
            {
                gameplayController.Active(show);
            }
        }

        public void ShowMainMenuPanel(bool show)
        {
            if (mainMenuController != null)
            {
                mainMenuController.Active(show);
            }
        }

        public void ShowGameOverPanel(bool show)
        {
            if (gameOverController != null)
            {
                gameOverController.Active(show);
            }
        }

        public void RestartGame()
        {
            gameOverController.OnRestartButtonClicked();
        }
    }
}
