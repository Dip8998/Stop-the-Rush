using STR.Enemy;
using System;
using System.Collections;
using UnityEngine;

namespace STR.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D bulletRigidBody;
        private Vector2 moveDirection = Vector2.up;
        private float damage;
        private Action<BulletController> onDespawn;
        private Coroutine lifeRoutine;

        private void Awake()
        {
            bulletRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Configure(Action<BulletController> onDespawn)
        {
            this.onDespawn = onDespawn;
        }

        public void Initialize(Vector2 direction, float damage, float lifetime)
        {
            this.damage = damage;

            if (direction.sqrMagnitude > 0f)
            {
                moveDirection = direction.normalized;
                transform.up = moveDirection;
            }

            if (lifeRoutine != null)
            {
                StopCoroutine(lifeRoutine);
            }

            lifeRoutine = StartCoroutine(LifeTimer(lifetime));
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
                enemy.Controller.TakeDamage(damage);

                Despawn();
            }
        }

        private IEnumerator LifeTimer(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            Despawn();
        }

        private void Despawn()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            if (lifeRoutine != null)
            {
                StopCoroutine(lifeRoutine);
                lifeRoutine = null;
            }

            if (bulletRigidBody != null)
            {
                bulletRigidBody.linearVelocity = Vector2.zero;
            }

            gameObject.SetActive(false);
            onDespawn?.Invoke(this);
        }
    }
}
