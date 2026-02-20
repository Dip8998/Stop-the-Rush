using STR.Enemy;
using STR.UI;
using UnityEngine;
using UnityEngine.UI;

namespace STR.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Image healthFillImage;
        private int _health = 100;

        private void Start()
        {
            SetFill(1f);
        }

        private void Update()
        {
            SetFill((float)_health / 100f);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void SetFill(float fillAmount)
        {
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = fillAmount;
            }
        }

        public void Die()
        {
            UIEvents.RaiseGameOverTriggered();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out EnemyView enemy))
            {
                Object.Destroy(enemy.gameObject);
                TakeDamage(enemy.Controller.EnemyData.Damage);
            }
        }
    }
}
