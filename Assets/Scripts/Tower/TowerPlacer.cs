using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

namespace STR.Tower
{
	public class TowerPlacer : MonoBehaviour
	{
		[SerializeField] private Tilemap towerPlacingTileMap;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            PlaceTowerOnTileMap();
        }

        private void PlaceTowerOnTileMap()
        {
            if (Input.GetMouseButtonDown(0) && mainCamera != null && towerPlacingTileMap != null)
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = towerPlacingTileMap.WorldToCell(worldPosition);

                if (TowerManager.Instance == null)
                {
                    return;
                }

                TowerScriptableObject selectedTower = TowerManager.Instance.SelectedTower;
                bool isTowerSelected = TowerManager.Instance.HasSelectedTower;

                bool isValidTile = towerPlacingTileMap.GetTile(cellPosition) != null;

                if (isTowerSelected && isValidTile)
                {
                    PlaceTower(cellPosition, selectedTower);
                    //TowerManager.Instance.DeselectTower(); // In future, Remove this line when trade system is implemented
                }
            }
        }

        private void PlaceTower(Vector3Int cellPosition, TowerScriptableObject selectedTower)
		{
            Tile tileForPlacement = towerPlacingTileMap.GetTile(cellPosition) as Tile;

            if(tileForPlacement != null && selectedTower != null && selectedTower.towerPrefab != null)
            {
                Vector3 worldPosition = towerPlacingTileMap.CellToWorld(cellPosition) + towerPlacingTileMap.tileAnchor;
                Instantiate(selectedTower.towerPrefab.gameObject, worldPosition, Quaternion.identity);
            }
        }
    }
}


