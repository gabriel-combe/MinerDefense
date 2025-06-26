using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject skinsPanel;
    [SerializeField]
    private GameObject upgradesPanel;
    

    // Load the game scene when Play button is pressed
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}
