using STR.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace STR.Wave
{
	public class WaveService : MonoBehaviour
	{
		[SerializeField] private EnemyScriptableObject enemy;
        [SerializeField] private WaveScriptableObject wave;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private List<Transform> waypoints;

        private List<EnemyController> spawnedEnemies = new List<EnemyController>();
        private EnemyController enemyController;
        private float spawnTimer;

        private void Update()
        {
            EnemyCreation();
        }

        private void EnemyCreation()
        {
            if (spawnedEnemies.Count < wave.EnemyCount)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer <= 0f)
                {
                    SpawnedEnemy();
                    spawnTimer = wave.SpawnInterval;
                }
            }

            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                spawnedEnemies[i].Tick();
            }
        }

        private void FixedUpdate()
        {
            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                spawnedEnemies[i].FixedTick();
            }
        }

        private void SpawnedEnemy()
        {
            enemyController = new EnemyController(enemy, enemySpawnPoint, waypoints);
            spawnedEnemies.Add(enemyController);
        }
    }
}
