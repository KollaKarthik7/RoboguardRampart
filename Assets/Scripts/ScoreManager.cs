using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public float elapsedTime;
    public int score;

    float highElapsedTime;
    int highScore;

    private void Start()
    {
        elapsedTime = 0f;
        score = 0;

        GetHighScore();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        scoreText.text = string.Format("Score: {00}", score);

        if(elapsedTime > highElapsedTime)
        {
            Debug.Log(highElapsedTime);
            highElapsedTime = elapsedTime;

            PlayerPrefs.SetFloat("HighTimer", highElapsedTime);
            PlayerPrefs.Save();
        }

        if(score > highScore)
        {
            Debug.Log(highScore);
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    void GetHighScore()
    {
        highElapsedTime = PlayerPrefs.GetFloat("HighTimer");
        highScore = PlayerPrefs.GetInt("HighScore");
    }
}
