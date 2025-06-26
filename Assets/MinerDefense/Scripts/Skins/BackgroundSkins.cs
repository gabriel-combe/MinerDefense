using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSkins : MonoBehaviour
{
    private SpriteRenderer backgroundImage;

    private GameManager gameManager;

    void Start()
    {
        backgroundImage = GetComponent<SpriteRenderer>();    
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        backgroundImage.sprite = gameManager.GetBackgroundSkin().image;
    }
}
