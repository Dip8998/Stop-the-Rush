using UnityEngine;
using UnityEngine.EventSystems;

namespace STR.Tower
{
    public class TowerUIController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TowerScriptableObject towerData;   
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (towerData == null || TowerManager.Instance == null)
            {
                return;
            }

            TowerManager.Instance.SelectTower(towerData);
        }
    }
}



