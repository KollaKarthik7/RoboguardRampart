using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject startPanel;
    public AudioSource source;
    public AudioClip buttonClick;
    public AudioClip themeSong;

    public Slider volumeSlider;    

    private void Start()
    {
        startPanel.SetActive(true);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }    

    private void Update()
    {
        SoundManager.volume = volumeSlider.value;
        source.volume = volumeSlider.value;
    }

    void OnVolumeChange(float volume)
    {
        Debug.Log("Volumr: " + volume);
        PlayerPrefs.SetFloat("Volume", volume);
        SoundManager.volume = volume;
        source.volume = volume;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ThemeSound()
    {
        source.PlayOneShot(themeSong);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonSound()
    {
        source.PlayOneShot(buttonClick);
    }
}
