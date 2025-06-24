using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GoldManager goldManager;

    [SerializeField]
    private BuildingDragDropManager dragDropManager;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private BuildingTypeSO building;
    
    private int cost;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        cost = building.startingCost;

        UpdateCostText();
    }

    private void Update()
    {
        if (goldManager.GetGold() <  cost)
            button.interactable = false;
        else
            button.interactable = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;

        dragDropManager.StartPlacingBuilding(building, this);
    }

    // Remove cost from the gold
    // Increase cost
    public void OnPlaced()
    {
        goldManager.RemoveGold(cost);

        cost *= 2;

        UpdateCostText();
    }

    public void UpdateCostText()
    {
        costText.text = cost.ToString();
    }
}
