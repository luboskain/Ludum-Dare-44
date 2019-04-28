using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    public float timeBetweenSpawns;
    int previousChoice;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5f, timeBetweenSpawns);
    }

    void SpawnEnemy()
    {
        int enemyChoice = Random.Range(0, enemies.Length);
        if (enemyChoice != previousChoice)
        {
            Instantiate(enemies[enemyChoice], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
            previousChoice = enemyChoice;
        }
        else
        {
            SpawnEnemy();
        }
    }
}
