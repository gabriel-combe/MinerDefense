using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private int gold;

    void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gold = gameManager.GetStartingGold();
    }

    public int GetGold() {  return gold; }
    public void AddGold(int gold) { this.gold += gold; }
}
