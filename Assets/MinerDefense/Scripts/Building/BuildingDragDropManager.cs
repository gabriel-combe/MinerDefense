using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.EventSystems;

public class BuildingDragDropManager : MonoBehaviour
{
    [SerializeField]
    private GridManager gridManager;

    private GameObject currentBuilding;
    private BuildingTypeSO currentBuildingData;
    private bool isDragging = false;

    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    public void StartPlacingBuilding(BuildingTypeSO building)
    {
        if (currentBuilding != null) Destroy(currentBuilding);

        currentBuildingData = building;
        currentBuilding = Instantiate(currentBuildingData.prefab);

        isDragging = true;
    }

    private void HandleFingerUpdate(LeanFinger finger)
    {
        if (!isDragging || finger.StartedOverGui) return;

        
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        if (!isDragging) return;
    }

}
