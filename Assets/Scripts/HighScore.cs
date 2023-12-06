using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    float elapsedTime;
    int score;

    private void Start()
    {
        UpdateHighScore();
    }

    private void Update()
    {      
        
    }

    void UpdateHighScore()
    {
        elapsedTime = PlayerPrefs.GetFloat("HighTimer");
        score = PlayerPrefs.GetInt("HighScore");

        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        scoreText.text = string.Format("Score: {00}", score);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighTimer", 0f);
        PlayerPrefs.SetInt("HighScore", 0);

        UpdateHighScore();
    }
}
