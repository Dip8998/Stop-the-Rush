using TMPro;
using UnityEngine;

namespace STR.Tower
{
    public class TowerManager : MonoBehaviour   
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        public static TowerManager Instance { get; private set; }

        private TowerScriptableObject selectedTower;
        public int Money { get; private set; } = 100;

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

        private void Start()
        {
            moneyText.text = $"Coins: {Money}";
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

        public bool CanAffordTower(TowerScriptableObject tower)
        {
            return tower != null && Money >= tower.cost;
        }

        public void SpendMoney(int amount)
        {
            Money -= amount;
            moneyText.text = $"Coins: {Money}";
        }

        public void EarnMoney(int amount)
        {
            Money += amount;
            moneyText.text = $"Coins: {Money}";
        }
    }
}
