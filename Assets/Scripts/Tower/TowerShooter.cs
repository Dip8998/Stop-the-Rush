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
        private LineRenderer lineRenderer;

        private void Start()
        {
            towerController = GetComponent<TowerController>();
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
                lineRenderer.useWorldSpace = true;
            }
        }

        private void Update()
        {
            if (towerController == null) return;

            RotateTowardsTargetforBullets();

            if (towerController.TowerData.towerType == TowerType.LaserGunTower)
            {
                ShootLaser();   
                return;
            }

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
            ShootBulletProjectile();
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
            if (towerController.Target == null)
            {
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = false;
                }
                return;
            }

            Vector3 direction = (towerController.Target.position - firePoint.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(
                firePoint.position,
                direction,
                towerController.TowerData.attackRange,
                towerController.TowerData.enemyLayer);

            if (hit.collider != null && hit.collider.TryGetComponent<EnemyView>(out var enemy))
            {
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, towerController.Target.position);
                }

                enemy.Controller.TakeDamage(towerController.TowerData.damage * Time.deltaTime);
            }
            else
            {
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = false;
                }
            }
        }

        private void RotateTowardsTargetforBullets()
        {  
            if (towerController.Target == null) return;

            Vector2 direction = (towerController.Target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
