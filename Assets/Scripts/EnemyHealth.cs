using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currenHealth;

    public GameObject explosionEffect;

    public Image healthbar;

    bool dead;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
        currenHealth = maxHealth;
    }

    private void Update()
    {
        float healthBarFill = currenHealth / maxHealth;
        healthbar.fillAmount = healthBarFill;

        if(currenHealth <= 0f && !dead)
        {
            dead = true;
            ExplosionEffect();
            soundManager.source.PlayOneShot(soundManager.enemyDeath);
            Destroy(gameObject, 0.1f);           
        }
    }

    public void ExplosionEffect()
    {
        GameObject effects = Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }


    public void Damage(float damage)
    {
        currenHealth -= damage;
    }
}
