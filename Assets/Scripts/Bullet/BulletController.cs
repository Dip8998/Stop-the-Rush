using STR.Enemy;
using UnityEngine;

namespace STR.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D bulletRigidBody;
        private Vector2 moveDirection = Vector2.up;
        private void Awake()
        {
            bulletRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector2 direction)
        {
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
                enemy.Controller.TakeDamage(1);

                Destroy(gameObject);
            }
        }
    }
}
