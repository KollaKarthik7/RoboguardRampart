using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform attackPoint;

    public int health = 3;
    public float environmentSpawnTimer = 10f;

    public GameObject[] healthBlock;
    public GameObject[] environments;

    GameObject player;

    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;
    public GameObject scorePanel;
    public GameObject teleportEffect;

    bool environmentSpawned;

    public int environmentNumber;
    public int tempNumber;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(health >= 0 && health < 3)
        {
            healthBlock[health].SetActive(false);
        }

        if(health <= 0)
        {
            Invoke("GameOver", 0.7f);
        }

        if(!environmentSpawned)
        {
            environmentSpawned = true;
            tempNumber = environmentNumber;
            SpawnEnvironment();
        }
    }

    public void SpawnEnvironment()
    {
        environmentNumber = Random.Range(0, environments.Length);

        if(environmentNumber == tempNumber)
        {
            SpawnEnvironment();
        }

        environments[environmentNumber].SetActive(true);
        Invoke("EnablePlayer", 0.3f);
        Invoke("SpawnNextEnvironment", environmentSpawnTimer);
    }

    public void SpawnNextEnvironment()
    {
        GameObject[] destroyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in destroyEnemies)
        {
            Destroy(obj);
        }

        Instantiate(teleportEffect, attackPoint.position, attackPoint.rotation);
        Invoke("DisablePlayer", 0.3f);
        Invoke("ResetEnvironment", 0.5f);
    }

    public void ResetEnvironment()
    {
        environments[environmentNumber].SetActive(false);
        environmentSpawned = false;
    }

    public void DisablePlayer()
    {
        player.SetActive(false);
        environments[environmentNumber].GetComponent<EnemySpawn>().enabled = false;
    }

    public void EnablePlayer()
    {
        player.SetActive(true);
        Invoke("SetSpawn", 0.3f);
    }

    public void SetSpawn()
    {
        environments[environmentNumber].GetComponent<EnemySpawn>().enabled = true;
    }

    void GameOver()
    {
        inGamePanel.SetActive(false);
        scorePanel.SetActive(false);
        GetComponent<ScoreManager>().enabled = false;
        gameOverPanel.SetActive(true);

        scoreText.text = GetComponent<ScoreManager>().scoreText.text;
        timerText.text = GetComponent<ScoreManager>().timeText.text;

        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
    }

    public void RestartGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
