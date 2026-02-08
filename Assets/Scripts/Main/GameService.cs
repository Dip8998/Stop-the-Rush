using STR.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace STR.Main
{
	public class GameService : MonoBehaviour
	{
		[SerializeField] private EnemyScriptableObject enemy;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private List<Transform> waypoints;

        private EnemyController enemyController;

        private void Awake()
        {
            enemyController = new EnemyController(enemy, enemySpawnPoint, waypoints);
        }
    }
}
