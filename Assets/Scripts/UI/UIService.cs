using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private GameWinController gameWinController;
        [SerializeField] private PauseController pauseController;
        [SerializeField] private NotificationController notificationController;

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

        private void OnEnable()
        {
            UIEvents.MoneyChanged += UpdateMoney;
            UIEvents.WaveChanged += UpdateWaveText;
            UIEvents.WaveTimerChanged += UpdateWaveTimer;
            UIEvents.WaveTimerVisibilityChanged += ShowWaveTimerText;
            UIEvents.NotificationRequested += HandleNotificationRequested;
            UIEvents.PlayRequested += HandlePlayRequested;
            UIEvents.PauseRequested += HandlePauseRequested;
            UIEvents.ResumeRequested += HandleResumeRequested;
            UIEvents.RestartRequested += HandleRestartRequested;
            UIEvents.MainMenuRequested += HandleMainMenuRequested;
            UIEvents.GameOverTriggered += HandleGameOverTriggered;
            UIEvents.GameWinTriggered += HandleGameWinTriggered;
        }

        private void OnDisable()
        {
            UIEvents.MoneyChanged -= UpdateMoney;
            UIEvents.WaveChanged -= UpdateWaveText;
            UIEvents.WaveTimerChanged -= UpdateWaveTimer;
            UIEvents.WaveTimerVisibilityChanged -= ShowWaveTimerText;
            UIEvents.NotificationRequested -= HandleNotificationRequested;
            UIEvents.PlayRequested -= HandlePlayRequested;
            UIEvents.PauseRequested -= HandlePauseRequested;
            UIEvents.ResumeRequested -= HandleResumeRequested;
            UIEvents.RestartRequested -= HandleRestartRequested;
            UIEvents.MainMenuRequested -= HandleMainMenuRequested;
            UIEvents.GameOverTriggered -= HandleGameOverTriggered;
            UIEvents.GameWinTriggered -= HandleGameWinTriggered;
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
                ShowGameWinPanel(false);
                ShowPausePanel(false);
                ShowNotificationPanel("", false);

                return;
            }

            ShowMainMenuPanel(true);
            ShowGameplayPanel(false);
            ShowGameOverPanel(false);
            ShowGameWinPanel(false);
            ShowPausePanel(false);
            ShowNotificationPanel("", false);
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

        public void ShowGameWinPanel(bool show)
        {
            if (gameWinController != null)
            {
                gameWinController.Active(show);
            }
        }

        public void ShowPausePanel(bool show)
        {
            if (pauseController != null)
            {
                pauseController.Active(show);
            }
        }
        
        public void ShowNotificationPanel(string message, bool show)
        {
            if (notificationController != null)
            {
                notificationController.ShowNotification(message, show);
            }
        }

        public void RestartGame()
        {
            SkipMainMenuOnce = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleNotificationRequested(string message)
        {
            ShowNotificationPanel(message, true);
        }

        private void HandlePlayRequested()
        {
            ShowMainMenuPanel(false);
            ShowGameplayPanel(true);
            ShowGameOverPanel(false);
            ShowGameWinPanel(false);
            ShowPausePanel(false);
            Time.timeScale = 1f;
        }

        private void HandlePauseRequested()
        {
            Time.timeScale = 0f;
            ShowGameplayPanel(false);
            ShowPausePanel(true);
        }

        private void HandleResumeRequested()
        {
            Time.timeScale = 1f;
            ShowPausePanel(false);
            ShowGameplayPanel(true);
        }

        private void HandleRestartRequested()
        {
            RestartGame();
        }

        private void HandleMainMenuRequested()
        {
            SkipMainMenuOnce = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleGameOverTriggered()
        {
            ShowGameplayPanel(false);
            ShowGameOverPanel(true);
            ShowGameWinPanel(false);
            ShowPausePanel(false);
            Time.timeScale = 0f;
        }

        private void HandleGameWinTriggered()
        {
            ShowGameplayPanel(false);
            ShowGameWinPanel(true);
            ShowGameOverPanel(false);
            ShowPausePanel(false);
            Time.timeScale = 0f;
        }
    }
}
