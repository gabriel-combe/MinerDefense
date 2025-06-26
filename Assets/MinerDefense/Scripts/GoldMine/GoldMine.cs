using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : BuildingObject
{
    [SerializeField]
    private int goldPerSecond = 1;

    private GoldManager goldManager;

    private void Start()
    {
        goldManager = GameObject.FindGameObjectWithTag("GoldManager").GetComponent<GoldManager>();
    }

    public override void OnPlaced()
    {
        base.OnPlaced();

        InvokeRepeating(nameof(GenerateGold), 1f, 1f);
    }

    public override void OnMoved()
    {
        base.OnMoved();

        CancelInvoke();
    }

    void GenerateGold()
    {
        goldManager.AddGold(goldPerSecond);
    }
}
