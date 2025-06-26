using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DollarDisplay : MonoBehaviour
{
    private TextMeshProUGUI dollarsText;
    private GameManager gameManager;

    void Start()
    {
        dollarsText = GetComponent<TextMeshProUGUI>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        UpdateDollarsText();
    }

    private void Update()
    {
        UpdateDollarsText();
    }

    public void UpdateDollarsText()
    {
        dollarsText.text = gameManager.GetDollars().ToString() + " $";
    }

}
