using UnityEngine;

namespace STR.Tower
{
    public class TowerManager : MonoBehaviour   
    {
        public static TowerManager Instance { get; private set; }

        private TowerScriptableObject selectedTower;

        public TowerScriptableObject SelectedTower => selectedTower;
        public bool HasSelectedTower => selectedTower != null;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SelectTower(TowerScriptableObject tower)
        {
            if (tower == null) return;

            selectedTower = tower;
        }

        public void DeselectTower()
        {
            selectedTower = null;
        }
    }
}
