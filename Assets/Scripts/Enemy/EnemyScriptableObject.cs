using UnityEngine;

namespace STR.Enemy
{
	[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
    public class EnemyScriptableObject : ScriptableObject
	{
		public EnemyView enemyPrefab;
        public EnemyType enemyType;
		public float health;
        public float moveSpeed;
		public float rotationSpeed;	
		public int rewardAmount;
		public int Damage;
    }
}
