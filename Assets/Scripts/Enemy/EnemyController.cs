using System.Collections.Generic;
using UnityEngine;

namespace STR.Enemy
{
    public class EnemyController
    {
        private EnemyScriptableObject enemyScriptableObject;
        private EnemyView enemyView;
        private List<Transform> waypoints;
        private Transform target;
        private int currentWaypointIndex = 0;
        private Rigidbody2D rb;
        private float currentHealth;

        public EnemyController(EnemyScriptableObject enemyScriptableObject, Transform spawnPosition, List<Transform> wayPoints)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            this.waypoints = wayPoints;
            InitializeEnemy(spawnPosition);
        }

        private void InitializeEnemy(Transform spawnPositon)
        {
            enemyView = Object.Instantiate(enemyScriptableObject.enemyPrefab, spawnPositon.position, Quaternion.identity);
            rb = enemyView.GetComponent<Rigidbody2D>();
            enemyView.Bind(this);
            target = waypoints[currentWaypointIndex];
            currentHealth = enemyScriptableObject.health;
        }

        public void Tick()
        {
            if (enemyView == null) return;

            if (Vector2.Distance(target.position, enemyView.transform.position) <= 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex == waypoints.Count)
                {
                    // Enemy reached the end of the path, you can handle it here (e.g., reduce player health)
                    Object.Destroy(enemyView.gameObject);
                    return;
                }
                else
                {
                    target = waypoints[currentWaypointIndex];
                }
            }
        }

        public void FixedTick()
        {
            if (enemyView == null) return;

            Vector2 direction = (target.position - enemyView.transform.position).normalized;

            rb.linearVelocity = direction * enemyScriptableObject.moveSpeed;
            
            RotateTowardDirection(direction);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void RotateTowardDirection(Vector2 direction)
        {
            if(direction.magnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                Quaternion angleRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                enemyView.transform.rotation = Quaternion.Slerp(enemyView.transform.rotation, angleRotation, Time.fixedDeltaTime * enemyScriptableObject.rotationSpeed);
            }
        }

        private void Die()
        {
            Object.Destroy(enemyView.gameObject);
        }
    }
}
