using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint; // The focal point is a GameObject in the project hierarchy
    public GameObject powerupIndicator;

    public float speed = 5.0f;
    private float powerupStrength = 20.0f;
    public bool hasPowerup = false; // False at the start of the game

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Get the player's Rigidbody component and apply it to the playerRb variable
        focalPoint = GameObject.Find("Focal Point"); // Find the GameObject in the project hierarchy named "Focal Point"
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // Store the player's vertical input in the forwardInput variable
        // Apply a forward force (relative to the focal point) to the player's Rigidbody based on the player's forward input
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // Make the powerup indicator always follow the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    // Called when two colliders interact with each other
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true); // Activate the powerup indicator
            Destroy(other.gameObject); // Destroy the powerup
            StartCoroutine(PowerupCountdownRoutine()); // Start the countdown timer
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7); // Wait for 7 seconds before executing following lines of code
        hasPowerup = false; // Turn off the powerup after the above timer expires
        powerupIndicator.gameObject.SetActive(false); // Deactivate the powerup indicator
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // enemy position - player position = vector from player to enemy
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
