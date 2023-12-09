using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject gameManager;

    public Transform[] spawnPoint;
    public float spawnInterval;

    GameObject enemy;
    Transform selectedPoint;
    float spawnTimerSubtractor;
    public int enemySize;
    int maxEnemySize;
    ScoreManager score;
    bool spawned;
    bool increased;
    bool spawnIncreased;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        score = gameManager.GetComponent<ScoreManager>();
        enemySize = 3;
        maxEnemySize = enemyPrefab.Length;
    }

    private void Update()
    {
        if(!spawnIncreased)
        {
            spawnIncreased = true;
            Invoke("DecreaseSpawnTime", 30f);
        }
        
        if (spawnTimerSubtractor > 1.2f)
        {
            spawnTimerSubtractor = 1.2f;
        }

        if(!increased)
        {
            increased = true;
            Invoke("EnemySizeIncreaser", 60f);
        }

        if(enemySize > maxEnemySize)
        {
            enemySize = maxEnemySize;
        }

        if (!spawned)
        {
            spawned = true;
            Spawn();
        }       
    }

    public void EnemySizeIncreaser()
    {
        enemySize++;
        increased = false;
    }

    public void DecreaseSpawnTime()
    {
        spawnTimerSubtractor += 0.2f;
        spawnIncreased = false;
    }

    public void Spawn()
    {
        if(enemyPrefab.Length > 0)
        {
            int enemyPrefabNum = Random.Range(0, enemySize);
            enemy = enemyPrefab[enemyPrefabNum];
        }

        if (spawnPoint.Length > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoint.Length);
            selectedPoint = spawnPoint[spawnPointIndex];
        }

        Instantiate(enemy, selectedPoint.position, Quaternion.identity);

        Invoke("ResetSpawn", spawnInterval - spawnTimerSubtractor);
    }

    public void ResetSpawn()
    {
        spawned = false;
    }
}
