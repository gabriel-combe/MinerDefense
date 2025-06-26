using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSkinButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI backgroundSkinName;

    private BackgroundSkinsTypeSO backgroundSkinsTypeSO;

    private Button backgroundSkinButton;

    private GameManager gameManager;

    private bool isActive = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        backgroundSkinButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (!isActive) return;

        if (!gameManager.GetBackgroundSkin().Equals(backgroundSkinsTypeSO))
            backgroundSkinButton.interactable = true;
        else 
            backgroundSkinButton.interactable = false;
    }

    // Set the background type of the button and set the name of the button
    public void SetBackgroundSkinType(BackgroundSkinsTypeSO backgroundSkinsTypeSO)
    {
        this.backgroundSkinsTypeSO = backgroundSkinsTypeSO;

        backgroundSkinName.text = this.backgroundSkinsTypeSO.nameString;
    
        isActive = true;
    }

    // Change the global background skin
    public void OnClicked()
    {
        gameManager.ChangeBackgroundSkin(this.backgroundSkinsTypeSO);
    }
}
