using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuildingLife : MonoBehaviour
{
    [Header("Base Upgrades Cost")]
    [SerializeField]
    private int baseUpgradesCost = 10;
    [SerializeField]
    private int incrUpgradeCost = 10;

    [Header("Utils")]
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private Button upgradeButton;

    private GameManager gameManager;

    private int upgradesCost;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        UpdateCost();
    }

    // Hide the upgrade button if there is not enough dollars
    private void Update()
    {
        if (upgradesCost > gameManager.GetDollars())
            upgradeButton.interactable = false;
        else
            upgradeButton.interactable = true;
    }

    public void UpdateCost()
    {
        upgradesCost = baseUpgradesCost + incrUpgradeCost * gameManager.GetBuildingLifeLevel();
        costText.text = upgradesCost.ToString() + " $";
        levelText.text = "Level " + gameManager.GetBuildingLifeLevel().ToString();
    }

    // On button clicked upgrade the stat and update the cost
    public void OnButtonClicked()
    {
        gameManager.UpgradeBuildingLife();

        gameManager.RemoveDollars(upgradesCost);

        UpdateCost();
    }
}
