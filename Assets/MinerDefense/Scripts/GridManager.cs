using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        GameObject backgroundTileParent = new GameObject("BackgroundTileGroup");

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                Instantiate(backgroundTile, grid.GetWorldPosition(x, y), Quaternion.identity, backgroundTileParent.transform);
    }

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
        }

        public void ClearBuildingObject()
        {
            this.buildingObject = null;
        }

        public bool CanBuild()
        {
            return this.buildingObject == null;
        }
    }

    private void Update()
    {
        //if (false)
        //{
        //    //grid.GetXY(, out int x, out int y);
        //}
    }
}
