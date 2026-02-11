using STR.Bullet;
using STR.Enemy;
using UnityEngine;

namespace STR.Tower
{
    public class TowerShooter : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;

        private TowerController towerController;
        private float shootTimer;

        private void Start()
        {
            towerController = GetComponent<TowerController>();
        }

        private void Update()
        {
            if (towerController == null) return;
            if (towerController.IsTargetInRange())
            {
                shootTimer -= Time.deltaTime;
                if(shootTimer <= 0f)
                {
                    Shoot();
                    shootTimer = towerController.TowerData.fireRate;
                }
            }
        }

        private void Shoot()
        {
            if (towerController == null) return;
            if(towerController.Target == null) return;

            if(towerController.TowerData.towerType == TowerType.LaserGunTower) ShootLaser();
            else ShootBulletProjectile();
        }

        private void ShootBulletProjectile()
        {
            GameObject projectileInstance = Instantiate(towerController.TowerData.bulletPrefab.gameObject, firePoint.position, transform.rotation);
            BulletController bulletController = projectileInstance.GetComponent<BulletController>();

            if (bulletController != null)
            {
                Transform target = towerController.Target;
                Vector2 direction = target != null
                    ? (target.position - firePoint.position).normalized
                    : (Vector2)firePoint.up;
                bulletController.Initialize(direction);
            }
            Destroy(projectileInstance, 2f);
        }

        private void ShootLaser()
        {
            if (towerController.Target == null) return;

            Vector3 direction = (towerController.Target.position - firePoint.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, towerController.TowerData.attackRange, towerController.TowerData.enemyLayer);

            if(hit.collider != null && hit.collider.TryGetComponent<EnemyView>(out var enemy))
            {
                enemy.Controller.TakeDamage(towerController.TowerData.damage);
            }
        }
    }
}
