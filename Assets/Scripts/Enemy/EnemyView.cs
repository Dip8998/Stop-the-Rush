using UnityEngine;

namespace STR.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController controller;

        public void Bind(EnemyController controller)
        {
            this.controller = controller;
        }

        private void Update()
        {
            controller?.Tick();
        }

        private void FixedUpdate()
        {
            controller?.FixedTick();
        }
    }
}
