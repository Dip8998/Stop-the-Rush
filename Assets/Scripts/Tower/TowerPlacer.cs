using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

namespace STR.Tower
{
	public class TowerPlacer : MonoBehaviour
	{
		[SerializeField] private Tilemap towerPlacingTileMap;

        private void Update()
        {
            PlaceTowerOnTileMap();
        }

        private void PlaceTowerOnTileMap()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = towerPlacingTileMap.WorldToCell(worldPosition);

                TowerScriptableObject selectedTower = TowerManager.Instance.GetSelectedTower();
                bool isTowerSelected = selectedTower != null;

                bool isValidTile = towerPlacingTileMap.GetTile(cellPosition) != null;

                if (isTowerSelected && isValidTile)
                {
                    PlaceTower(cellPosition);
                    TowerManager.Instance.DeselectTower();
                }
            }
        }

        private void PlaceTower(Vector3Int cellPosition)
		{
            Tile tileForPlacement = towerPlacingTileMap.GetTile(cellPosition) as Tile;

            if(tileForPlacement != null)
            {
                TowerScriptableObject selectedTower = TowerManager.Instance.GetSelectedTower();

                if (selectedTower == null) return;
                
                Vector3 worldPosition = towerPlacingTileMap.CellToWorld(cellPosition) + towerPlacingTileMap.tileAnchor;
                Instantiate(selectedTower.towerPrefab, worldPosition, Quaternion.identity);
            }
        }
    }
}


