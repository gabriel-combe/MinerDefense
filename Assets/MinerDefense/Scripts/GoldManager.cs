using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private int gold;

    [SerializeField]
    private TextMeshProUGUI goldText;

    void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gold = gameManager.GetStartingGold();

        UpdateGoldText();
    }

    public int GetGold() {  return gold; }
    public void AddGold(int gold) 
    {
        this.gold += gold;
        UpdateGoldText();
    }
    public void RemoveGold(int gold)
    {
        this.gold -= gold;
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }
}
