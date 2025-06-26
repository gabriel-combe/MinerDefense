using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [Header("Grid Properties")]
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private float cellSize;
    [SerializeField]
    private Vector3 originPosition;
    [SerializeField]
    private GameObject backgroundTile;

    private CustomGrid<GridTile> grid;

    private void Start()
    {
        grid = new CustomGrid<GridTile>(width, height, cellSize, originPosition, (CustomGrid<GridTile> g, int x, int y) => new GridTile(g, x, y));

        // Display empty tile to show possible placements
        GameObject backgroundTileParent = new GameObject("BackgroundTileGroup");

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                Instantiate(backgroundTile, grid.GetWorldPosition(x, y) + new Vector3(0, 0, 1), Quaternion.identity, backgroundTileParent.transform);
    }

    // GGrid tile class that store the position, grid ref, and buildingObject on the tile
    public class GridTile
    {
        private CustomGrid<GridTile> grid;
        private int x;
        private int y;
        private BuildingObject buildingObject;

        public GridTile(CustomGrid<GridTile> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetBuildingObject(BuildingObject buildingObject)
        {
            this.buildingObject = buildingObject;
            this.buildingObject.SetOrigin(new Vector2Int(x, y));
        }

        public void ClearBuildingObject()
        {
            buildingObject = null;
        }

        public bool CanBuild()
        {
            return buildingObject == null;
        }
    }

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }

    public bool IsEmpty()
    {
        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
                if (!grid.GetGridObject(x, y).CanBuild()) return false;
        
        return true;
    }

    public bool IsPlacementValid(Vector3 buildingPosition, BuildingTypeSO buildingData)
    {
        grid.GetXY(buildingPosition, out int xPos, out int yPos);

        if (xPos == -1 || yPos == -1) return false;

        List<Vector2Int> gridPositionList = buildingData.GetGridPositionList(new Vector2Int(xPos, yPos));

        // Check if we can build at this position on the grid
        foreach(Vector2Int gridPosition in gridPositionList)
            if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) return false;

        return true;
    }

    public Vector3 AlignToGrid(Vector3 position, BuildingTypeSO buildingData)
    {
        // Offset the position to compensate the centered pivot point of the buildings
        grid.GetXY(position + new Vector3(buildingData.width * cellSize * 0.5f, buildingData.height * cellSize * 0.5f), out int x, out int y);
        
        if (x == -1 || y == -1) return position;

        return grid.GetWorldPosition(x, y);
    }

    public void PlaceBuilding(BuildingObject buildingObject)
    {
        grid.GetXY(buildingObject.transform.position, out int xPos, out int yPos);

        List<Vector2Int> gridPositionList = buildingObject.GetBuildingType().GetGridPositionList(new Vector2Int(xPos, yPos));

        foreach (Vector2Int gridPosition in gridPositionList)
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetBuildingObject(buildingObject);

        buildingObject.OnPlaced();
    }
}
