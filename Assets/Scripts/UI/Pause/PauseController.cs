using UnityEngine;
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
            UIEvents.RaiseResumeRequested();
        }

        private void OnRestartButtonClicked()
        {
            UIEvents.RaiseRestartRequested();
        }

        private void OnMainMenuButtonClicked()
        {
            UIEvents.RaiseMainMenuRequested();
        }

        public void Active (bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
