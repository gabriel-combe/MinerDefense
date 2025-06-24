using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.EventSystems;

public class BuildingDragDropManager : MonoBehaviour
{
    [SerializeField]
    private GridManager gridManager;

    private BuildingObject currentBuilding;
    private BuildingTypeSO currentBuildingData;
    private BuildingButton currentButton;
    private bool isDragging = false;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    public void StartPlacingBuilding(BuildingTypeSO building, BuildingButton button)
    {
        if (currentBuilding != null) Destroy(currentBuilding);

        currentBuildingData = building;
        currentBuilding = BuildingObject.Create(building);
        currentButton = button;

        isDragging = true;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (isDragging || finger.StartedOverGui) return;

        isDragging = true;
    }

    private void HandleFingerUpdate(LeanFinger finger)
    {
        if (!isDragging) return;

        // Snap to grid if the aligned position is valid otherwise follow the finger
        Debug.Log(finger.GetWorldPosition(10));
        Vector3 alignedPos = gridManager.AlignToGrid(finger.GetWorldPosition(10));

        currentBuilding.transform.position = alignedPos;
    }

    // When finger up place the building on the grid and update the building cost
    // Otherwise destroy the dragged building and keep the cost as it is
    private void HandleFingerUp(LeanFinger finger)
    {
        if (!isDragging) return;

        if (gridManager.IsPlacementValid(currentBuilding.transform.position, currentBuildingData))
        {
            gridManager.PlaceBuilding(currentBuilding);

            currentButton.OnPlaced();
        } else
        {
            Destroy(currentBuilding.GetBuildingGameObject());
        }

        currentBuilding = null;
        currentButton = null;

        isDragging = false;
    }

}
