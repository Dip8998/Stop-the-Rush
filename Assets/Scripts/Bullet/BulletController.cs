using STR.Enemy;
using STR.Tower;
using UnityEngine;

namespace STR.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D bulletRigidBody;
        private Vector2 moveDirection = Vector2.up;
        private TowerController towerController;

        private void Awake()
        {
            bulletRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector2 direction, TowerController towerController)
        {
            this.towerController = towerController;

            if (direction.sqrMagnitude > 0f)
            {
                moveDirection = direction.normalized;
                transform.up = moveDirection;
            }
        }

        private void FixedUpdate()
        {
            if (bulletRigidBody == null)
            {
                return;
            }

            bulletRigidBody.linearVelocity = moveDirection * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyView>(out var enemy))
            {
                enemy.Controller.TakeDamage(towerController.TowerData.damage);

                Destroy(gameObject);
            }
        }
    }
}
