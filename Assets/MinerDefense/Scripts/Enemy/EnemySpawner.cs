using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameOverManager gameOverManager;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 30f, 30f);
    }

    // Spawn more enemy each waves and randomize their spawning position
    void SpawnEnemy()
    {
        gameOverManager.IncreaseWave();

        for(int i = 0; i < gameOverManager.GetWaveNumber(); i++)
            Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-1f, 1f)), Quaternion.identity);
    }
}
