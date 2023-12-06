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
    bool spawned;
    float spawnTimerSubtractor;
    int enemySize;
    int maxEnemySize;
    ScoreManager score;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        score = gameManager.GetComponent<ScoreManager>();
        enemySize = 3;
        maxEnemySize = enemyPrefab.Length;
    }

    private void Update()
    {
        spawnTimerSubtractor = (gameManager.GetComponent<ScoreManager>().elapsedTime / 30f) * 0.25f;
        if (spawnTimerSubtractor > 1.2f)
        {
            spawnTimerSubtractor = 1.2f;
        }

        if(score.elapsedTime % 30 == 0)
        {
            enemySize++;
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
