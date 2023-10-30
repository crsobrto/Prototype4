using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float speed = 5.0f; // Default speed = 5

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); // Find the "Player" GameObject in the project hierarchy
    }

    // Update is called once per frame
    void Update()
    {
        // player position - enemy position = vector between player and enemy
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10) // If the enemy's y position is less than -10
        {
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
