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

            if (!IsTargetInRange())
            {
                target = null;
            }
        }

        private void FindTarget()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerData.attackRange, Vector2.zero, 0f, towerData.enemyLayer);

            if(hits.Length > 0)
            {
                target = hits[0].transform;
            }
        }

        public bool IsTargetInRange()
        {
            if (towerData == null || target == null) return false;

            return Vector2.Distance(transform.position, target.position) <= towerData.attackRange;
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
