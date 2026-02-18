using TMPro;
using UnityEngine;

namespace STR.UI
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moenyText;
        [SerializeField] private TextMeshProUGUI waveNumberText;
        [SerializeField] private TextMeshProUGUI waveTimerText;

        public void UpdateMoney(int money)
        {
            if (moenyText != null)
            {
                moenyText.text = $"Money: {money}";
            }
        }

        public void UpdateWaveText(int waveNumber)
        {
            if (waveNumberText != null)
            {
                waveNumberText.text = $"Wave: {waveNumber}";
            }
        }

        public void UpdateWaveTimer(float timeRemaining)
        {
            if (waveTimerText != null)
            {
                waveTimerText.text = $"00: {timeRemaining:F1}s";
            }
        }

        public void ShowWaveTimerText(bool active)
        {
            waveTimerText.gameObject.SetActive(active);
        }

        public void Active(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
