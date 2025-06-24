using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Buildings")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public GameObject prefab;
    public int width;
    public int height;
    public int startingCost;

    public List<Vector2Int> GetGridPositionList(Vector2Int position)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                gridPositionList.Add(position + new Vector2Int(x, y));

        return gridPositionList;
    }
}
