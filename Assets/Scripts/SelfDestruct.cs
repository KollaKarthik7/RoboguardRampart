using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public GameObject explosionEffect;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            gameObject.tag = "Untagged";
            GetComponent<EnemyHealth>().enabled = false;
            Invoke("Explosion", 0.2f);
        }
    }

    public void Explosion()
    {
        soundManager.source.PlayOneShot(soundManager.playerDamage);
        FindObjectOfType<GameManager>().GetComponent<GameManager>().health--;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
