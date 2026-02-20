using System.Collections.Generic;
using UnityEngine;

namespace STR.Enemy
{
    public static class EnemyRegistry
    {
        private static readonly List<EnemyView> enemies = new List<EnemyView>();

        public static void Register(EnemyView enemy)
        {
            if (enemy == null || enemies.Contains(enemy))
            {
                return;
            }

            enemies.Add(enemy);
        }

        public static void Unregister(EnemyView enemy)
        {
            if (enemy == null)
            {
                return;
            }

            enemies.Remove(enemy);
        }

        public static Transform GetClosestTarget(Vector3 position, float range)
        {
            if (enemies.Count == 0)
            {
                return null;
            }

            float rangeSqr = range * range;
            Transform closest = null;

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                EnemyView enemy = enemies[i];
                if (enemy == null)
                {
                    enemies.RemoveAt(i);
                    continue;
                }

                Vector3 offset = enemy.transform.position - position;
                float distanceSqr = offset.sqrMagnitude;

                if (distanceSqr <= rangeSqr)
                {
                    rangeSqr = distanceSqr;
                    closest = enemy.transform;
                }
            }

            return closest;
        }
    }
}
