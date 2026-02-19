using System.Collections;
using TMPro;
using UnityEngine;

namespace STR.UI
{
    public class NotificationController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;


        public void ShowNotification(string message, bool active)
        {
            text.text = message;
            gameObject.SetActive(active);
            UIService.Instance.StartCoroutine(HideNotificationAfterDelay(2f));
        }

        IEnumerator HideNotificationAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}
