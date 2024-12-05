using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))] // Ensures the object has a TrailRenderer and BoxCollider attached
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager to check if the game is active
    private Camera cam; // Reference to the camera for converting screen position to world position
    private Vector3 mousePos; // Position of the mouse in world space
    private TrailRenderer trail; // Reference to the TrailRenderer component to display a trail while swiping
    private BoxCollider col; // Reference to the BoxCollider component to detect collisions
    private bool swiping = false; // Bool to track whether the player is swiping or not


    // Awake is called when the script is first initialized
    void Awake()
    {
        cam = Camera.main; // Get the main camera
        trail = GetComponent<TrailRenderer>(); // Get the TrailRenderer component
        col = GetComponent<BoxCollider>(); // Get the BoxCollider component
        trail.enabled = false; // Initially disable the trail renderer
        col.enabled = false; // Initially disable the collider

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get the GameManager instance
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive) // Only allow actions if the game is active
        {
            if(Input.GetMouseButtonDown(0)) // Detects when the mouse button is pressed
            {
                swiping = true; // Start the swipe action
                UpdateComponents(); // Update the trail and collider components
            }
            else if(Input.GetMouseButtonUp(0)) // Detects when the mouse button is released
            {
                swiping = false; // Stop the swipe action
                UpdateComponents(); // Update the trail and collider components
            }
            if (swiping) // If swiping is active, update the mouse position
            {
                UpdateMousePosition();
            }
        }
    }

    // Updates the mouse position in the game world
    void UpdateMousePosition()
    {
        // Convert the screen space mouse position to world space
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos; // Update the object's position to match the mouse position
    }

    // Updates the trail renderer and collider based on whether swiping is active
    void UpdateComponents()
    {
        trail.enabled = swiping; // Enable or disable the trail renderer
        col.enabled = swiping; // Enable or disable the collider
    }

    // Called when the object collides with another object
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>()) // Check if the collided object is a Target
        {
            // Destroy the target object
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
