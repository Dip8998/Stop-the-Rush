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
        }

        public void Tick()
        {
            if (Vector2.Distance(target.position, enemyView.transform.position) <= 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex == waypoints.Count)
                {
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
            Vector2 direction = (target.position - enemyView.transform.position).normalized;

            rb.linearVelocity = direction * enemyScriptableObject.moveSpeed; 
        }
    }
}
