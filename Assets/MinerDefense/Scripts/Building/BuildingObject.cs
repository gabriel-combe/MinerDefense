using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObject : MonoBehaviour
{
    // What to do when the building is placed on the grid
    public virtual void OnPlaced() { }

    // What to do when the building is moved from one cell to another
    public virtual void OnMoved() { }
}
