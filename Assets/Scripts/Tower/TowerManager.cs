using UnityEngine;

namespace STR.Tower
{
    public class TowerManager : MonoBehaviour   
    {
        public static TowerManager Instance { get; private set; }

        [HideInInspector] public TowerScriptableObject selectedTower = null;
        [HideInInspector] public bool isTowerSelectedForPlacement = false;

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
            selectedTower = tower;
            isTowerSelectedForPlacement = true;
        }

        public void DeselectTower()
        {
            selectedTower = null;
            isTowerSelectedForPlacement = false;
        }

        public TowerScriptableObject GetSelectedTower()
        {
            return selectedTower;
        }
    }
}
