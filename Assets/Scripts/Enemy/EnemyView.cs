using UnityEngine;

namespace STR.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController controller;
        public EnemyController Controller => controller;

        public void Bind(EnemyController controller)
        {
            this.controller = controller;
        }
    }
}
