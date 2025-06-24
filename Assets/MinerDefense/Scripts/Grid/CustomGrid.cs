using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public CustomGrid(int width, int height, float cellSize, Vector3 originPosition, Func<CustomGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            return default(TGridObject);

        return gridArray[x, y];
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * this.cellSize + this.originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = (int)((worldPosition - originPosition).x / this.cellSize);
        y = (int)((worldPosition - originPosition).y / this.cellSize);

        if (x < 0 || x >= this.width) x = -1;
        if (y < 0 || y >= this.height) y = -1;
    }
}
