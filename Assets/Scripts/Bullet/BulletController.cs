using STR.Enemy;
using UnityEngine;

namespace STR.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D bulletRigidBody;
        private void Awake()
        {
            bulletRigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            bulletRigidBody.linearVelocity = transform.up * speed;
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
