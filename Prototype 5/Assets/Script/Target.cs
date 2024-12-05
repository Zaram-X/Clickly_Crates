using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; // Reference to the Rigidbody component for applying forces and torques
    private GameManager gameManager; // Reference to the GameManager to interact with game state
    private float minSpeed = 12; // Minimum upward force (speed)
    private float maxSpeed = 16;  // Maximum upward force (speed)
    private float maxTorque = 10; // Maximum rotational torque to make the target spin
    private float xRange = 4;  // Horizontal spawn range (random value between -xRange and xRange)
    private float ySpwanPos = 3.0f; // Vertical spawn position for targets

    public int pointValue; // Points awarded for destroying this target
    public ParticleSystem explosionParticle; // Particle system for explosion effect when the target is destroyed

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the target
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Find the GameManager component

        // Apply random upward force and random torque to the target
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // Set a random position for the target
        transform.position = RandomPosition(); 
    }

    // Update is called once per frame (currently not used)
    void Update()
    {

    }

    /*
    // This method is commented out but can be used to destroy the target when clicked
    private void OnMouseDown() 
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject); // Destroy the target
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // Instantiate explosion particle effect
            gameManager.UpdateScore(pointValue); // Update the score
        }
    }
    */

    // Called when the target collides with a trigger
    private void OnTriggerEnter(Collider other)  
    {
        Destroy(gameObject); // Destroy the target
        // If the target is not tagged as "Bad" and the game is active, update lives
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.UpdateLives(-1); // Decrease lives
        }
    }

    // Public method to destroy the target and update the score
    public void DestroyTarget()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject); // Destroy the target
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // Play explosion effect
            gameManager.UpdateScore(pointValue); // Update the score
        }
    }

    // Returns a random upward force applied to the target
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(maxSpeed, minSpeed); // Random upward force between minSpeed and maxSpeed
    }

    // Returns a random position within the specified horizontal range
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), -ySpwanPos); // Random x position and fixed y position
    }

    // Returns a random torque value for the target's rotation
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque); // Random torque within the specified range
    }
}
