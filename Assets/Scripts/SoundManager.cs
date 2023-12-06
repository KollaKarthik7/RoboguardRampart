using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public static float volume;

    public AudioSource source;

    public AudioClip turretSound;
    public AudioClip enemyDeath;
    public AudioClip playerDamage;
    public AudioClip playerDeath;
    public AudioClip buttonClick;

    private void Start()
    {
        soundManager = gameObject.GetComponent<SoundManager>();
    }

    private void Update()
    {
        source.volume = volume;
    }

    public void ButtonSound()
    {
        source.PlayOneShot(buttonClick);
    }
}
