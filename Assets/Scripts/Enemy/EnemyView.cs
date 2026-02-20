using UnityEngine;

namespace STR.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController controller;
        public EnemyController Controller => controller;

        private void OnEnable()
        {
            EnemyRegistry.Register(this);
        }

        private void OnDisable()
        {
            EnemyRegistry.Unregister(this);
        }

        public void Bind(EnemyController controller)
        {
            this.controller = controller;
        }
    }
}
