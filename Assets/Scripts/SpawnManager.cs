using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private PlayerController playerControllerScript;
    public float randomRange = 9;
    public int enemyCount;
    public int waveNumber = 1;


    void Start()
    {
        SpawnEnemeWave(waveNumber);
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        CreateNewEnemy();
    }

    private void SpawnEnemeWave(int enemiesToSpawn)
    {
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

        }
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-randomRange, randomRange);
        float spawnZ = Random.Range(-randomRange, randomRange);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;

    }

    private void CreateNewEnemy()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (playerControllerScript.isGameOver == false)
        {
            if (enemyCount == 0)
            {
                waveNumber++;
                SpawnEnemeWave(waveNumber);
                Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);

            }
        }
    }
}
