using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObject : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSO;
    private Vector2Int origin;
    private bool isOnGrid = false;

    private int life;
    
    public static BuildingObject Create(BuildingTypeSO buildingTypeSO)
    {
        GameObject buildingObjectGameObject = Instantiate(buildingTypeSO.prefab);

        BuildingObject buildingObject = buildingObjectGameObject.GetComponent<BuildingObject>();

        buildingObject.buildingTypeSO = buildingTypeSO;

        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingObject.life = gameManager.GetBuildingLife();

        return buildingObject;
    }

    public void SetOrigin(Vector2Int origin)
    {
        this.origin = origin;
    }

    public Vector2Int GetOrigin()
    {
        return origin;
    }

    public BuildingTypeSO GetBuildingType()
    {
        return buildingTypeSO;
    }

    public bool IsOnGrid() { return isOnGrid; }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0 )
            Destroy(gameObject);
    }

    // What to do when the building is placed on the grid
    public virtual void OnPlaced() { isOnGrid = true; }

    // What to do when the building is moved from one cell to another
    public virtual void OnMoved() { isOnGrid = false; }
}
