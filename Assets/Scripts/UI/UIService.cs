using UnityEngine;

namespace STR.UI
{
    public class UIService : MonoBehaviour
    {
        private static UIService instance;
        public static UIService Instance => instance;

        [SerializeField] private GameplayController gameplayController;

        [SerializeField] private MainMenuController mainMenuController;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private void Start()
        {
            ShowMainMenuPanel(true);
            ShowGameplayPanel(false);
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
    }
}
