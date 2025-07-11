using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI dollarRewardText;
    [SerializeField]
    private int dollarBaseReward = 10;

    [SerializeField]
    private GridManager gridManager;

    private GameManager gameManager;

    private int waveNumber = 0;
    private bool startGame = false;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Invoke(nameof(StartGame), 30f);
    }

    // Check if we are game over
    void Update()
    {
        if (startGame && gridManager.IsEmpty())
        {
            startGame = false;
            
            GameOver();
        }
    }

    public void IncreaseWave() {  waveNumber++; }

    public int GetWaveNumber() { return waveNumber; }

    // Display the gameover screen and give a reward to the player
    // Then go to main menu
    private void GameOver()
    {
        gameOverScreen.SetActive(true);

        int dollarReward = dollarBaseReward * waveNumber;
        dollarRewardText.text = dollarReward.ToString() + " $";

        gameManager.AddDollars(dollarReward);

        GameManager.Save();

        Debug.Log("Game Over");

        Invoke(nameof(MainMenu), 2f);
    }

    void StartGame()
    {
        startGame = true;
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
