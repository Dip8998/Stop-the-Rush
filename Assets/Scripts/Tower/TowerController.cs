using STR.Enemy;
using UnityEngine;

namespace STR.Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerScriptableObject towerData;

        public TowerScriptableObject TowerData => towerData;
        public Transform Target => target;
        private Transform target;

        private void Update()
        {
            if (towerData == null) return;

            if (target == null)
            {
                FindTarget();
                return;
            }

            RotateTowardsTarget();

            if (IsTargetOutOfReleaseRange())
            {
                target = null;
            }
        }

        private void FindTarget()
        {
            target = EnemyRegistry.GetClosestTarget(transform.position, towerData.attackRange);
        }

        public bool IsTargetInRange()
        {
            if (towerData == null || target == null) return false;

            float shootRange = towerData.attackRange + Mathf.Max(0f, towerData.targetReleaseBuffer);
            float shootRangeSqr = shootRange * shootRange;
            return (target.position - transform.position).sqrMagnitude <= shootRangeSqr;
        }

        private bool IsTargetOutOfReleaseRange()
        {
            if (towerData == null || target == null) return true;

            float releaseRange = towerData.attackRange + towerData.targetReleaseBuffer;
            float releaseRangeSqr = releaseRange * releaseRange;
            return (target.position - transform.position).sqrMagnitude > releaseRangeSqr;
        }

        private void RotateTowardsTarget()
        {
            Vector2 direction = (target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion angleRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, angleRotation, Time.deltaTime * towerData.towerRotationSpeed);
        }

        private void OnDrawGizmos()
        {
            if (towerData == null) return;

#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, towerData.attackRange);
#endif
        }
    }
}
