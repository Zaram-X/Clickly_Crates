using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button; // Reference to the Button component
    private GameManager gameManager; // Reference to the GameManager component
    public int difficulty; // The difficulty level associated with this button

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); // Get the Button component attached to the GameObject
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get the GameManager instance
        button.onClick.AddListener(SetDiffculty); // Add a listener to the button's onClick event
    }

    // Update is called once per frame (not used in this script)
    void Update()
    {
        
    }

    // Method to set the game difficulty when the button is clicked
    void SetDiffculty()
    {
        Debug.Log(button.gameObject.name + " was clicked!"); // Log the button click for debugging
        gameManager.StartGame(difficulty); // Call the StartGame method in GameManager with the selected difficulty
    }
}
