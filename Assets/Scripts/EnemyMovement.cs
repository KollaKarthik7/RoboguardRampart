using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerPosition;

    GameObject gameManager;

    public float moveSpeed;
    float speedAdder;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;       
        if (playerPosition == null)
        {
            Destroy(gameObject);
        }


        transform.LookAt(playerPosition);
    }

    private void Update()
    {
        speedAdder = gameManager.GetComponent<ScoreManager>().elapsedTime / 10f;
        if(speedAdder > 7)
        {
            speedAdder = 7f;
        }

        if (playerPosition != null)
        {
            Vector3 playerDir = (playerPosition.position - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, (moveSpeed + speedAdder) * Time.deltaTime);
        }
    }
}
