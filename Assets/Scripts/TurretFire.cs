using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    public GameObject turretHead;
    public float damage;
    public Transform muzzleFlashPos;
    public GameObject muzzleFlash;
    public GameObject hitEffect;

    public GameObject gameManager;

    float damageAdder;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            LookEnemy();
        }

        damageAdder = GameObject.Find("GameManager").GetComponent<ScoreManager>().elapsedTime / 120f;
        damageAdder = Mathf.RoundToInt(damageAdder);

        if(damageAdder > 2)
        {
            damageAdder = 2;
        }
    }

    void LookEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit1;

                if (Physics.Raycast(ray1, out hit1) && hit1.collider.CompareTag("Enemy"))
                {
                    turretHead.transform.LookAt(hit1.collider.gameObject.transform);
                    Shoot();
                }
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy"))
        {
            turretHead.transform.LookAt(hit.collider.gameObject.transform);
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit1;

                if (Physics.Raycast(ray1, out hit1))
                {
                    Debug.Log(hit1.collider.gameObject.name);
                    hit1.collider.GetComponent<EnemyHealth>().Damage(damage + damageAdder);

                    if (hit1.collider.GetComponent<EnemyHealth>().currenHealth <= 0f)
                    {
                        GameObject.Find("GameManager").GetComponent<ScoreManager>().score++;
                    }
                }
            }
        }

        RaycastHit hit;

        soundManager.source.PlayOneShot(soundManager.turretSound);
        Instantiate(muzzleFlash, muzzleFlashPos.position, Quaternion.identity);

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            hit.collider.GetComponent<EnemyHealth>().Damage(damage + damageAdder);

            if(hit.collider.GetComponent<EnemyHealth>().currenHealth <= 0f)
            {
                GameObject.Find("GameManager").GetComponent<ScoreManager>().score++;
            }
        }
    }
}
