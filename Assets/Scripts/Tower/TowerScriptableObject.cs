using UnityEngine;

namespace STR.Tower
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
    public class TowerScriptableObject : ScriptableObject
    {
        public TowerType towerType;
        public GameObject towerPrefab;
    }
}
