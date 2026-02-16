using STR.Bullet;
using UnityEngine;

namespace STR.Tower
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
    public class TowerScriptableObject : ScriptableObject
    {
        public TowerType towerType;
        public TowerController towerPrefab;
        public BulletController bulletPrefab;
        public float damage;
        public float fireRate;
        public float attackRange;
        public float towerRotationSpeed;
        public float targetReleaseBuffer;
        public int cost;
        public LayerMask enemyLayer;
    }
}
