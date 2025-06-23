using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private List<BuildingTypeSO> buildings;

    private Dictionary<string, BuildingTypeSO> buildingsDictionary = new Dictionary<string, BuildingTypeSO>();

    private void Start()
    {
        foreach (var buildingType in buildings)
        {
            buildingsDictionary.Add(buildingType.nameString, buildingType);
        }
    }

    public void SpawnBuilding(string buildingKey)
    {
        Debug.Log("AAAAAAAAAAAAAAA");
    }
}
