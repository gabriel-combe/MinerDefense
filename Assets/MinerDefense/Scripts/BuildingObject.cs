using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSO;
    private Vector2Int origin;

    public static BuildingObject Create(Vector3 worldPosition, Vector2Int origin, BuildingTypeSO buildingTypeSO)
    {
        GameObject buildingObjectGameObject = Instantiate(buildingTypeSO.prefab, worldPosition, Quaternion.identity);

        BuildingObject buildingObject = buildingObjectGameObject.GetComponent<BuildingObject>();

        buildingObject.buildingTypeSO = buildingTypeSO;
        buildingObject.origin = origin;

        return buildingObject;
    }
}
