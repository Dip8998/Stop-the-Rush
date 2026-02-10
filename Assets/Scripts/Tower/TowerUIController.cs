using UnityEngine;
using UnityEngine.EventSystems;

namespace STR.Tower
{
    public class TowerUIController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TowerScriptableObject towerData;   
        
        public void OnPointerDown(PointerEventData eventData)
        {
            TowerManager.Instance.SelectTower(towerData);
        }
    }
}



