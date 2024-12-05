using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // To access the TextMeshPro class for UI text
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; // List of target prefabs to spawn
    public TextMeshProUGUI scoreText; // Text for displaying the score
    public TextMeshProUGUI gameOverText; // Text for displaying the game over message
    public TextMeshProUGUI liveText; // Text for displaying the number of lives
    public Button restartButton; // Button for restarting the game
    public GameObject titleScreen; // Title screen UI element
    public GameObject pauseScreen; // Pause screen UI element
    private bool paused; // To track if the game is paused
    public bool isGameActive; // To track if the game is currently active
    private int score; // The player's score
    private int lives; // The number of lives the player has
    private float spawnRate = 2.0f; // Time between spawning targets

    // Start is called before the first frame update
    void Start()
    {
        // Initialization can be placed here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed to toggle pause state
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }
    }

    // Coroutine for spawning targets at intervals
    IEnumerator SpawnTarget()
    {
        // Keep spawning targets while the game is active
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count); // Randomly select a target from the list
            Instantiate(targets[index]); // Spawn the target
        }
    }

    // Method to update the score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Add the score value
        scoreText.text = "Score: " + score; // Update the UI text
    }

    // Method to update the number of lives
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange; // Modify the lives
        liveText.text = "Lives: " + lives; // Update the UI text

        // If lives reach 0, trigger GameOver
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // Method to handle game over logic
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); // Show the restart button
        gameOverText.gameObject.SetActive(true); // Show the game over text
        isGameActive = false; // Set the game as inactive
    }

    // Method to restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Method to start the game with the selected difficulty
    public void StartGame(int difficulty)
    {
        isGameActive = true; // Set the game as active
        score = 0; // Reset the score
        spawnRate /= difficulty; // Adjust the spawn rate based on difficulty

        StartCoroutine(SpawnTarget()); // Start spawning targets
        UpdateScore(0); // Display the initial score
        UpdateLives(3); // Set the initial lives

        titleScreen.gameObject.SetActive(false); // Hide the title screen
    }

    // Method to toggle the paused state
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true; // Set the game to paused
            pauseScreen.SetActive(true); // Show the pause screen
            Time.timeScale = 0; // Stop the game time
        }
        else
        {
            paused = false; // Set the game to unpaused
            pauseScreen.SetActive(false); // Hide the pause screen
            Time.timeScale = 1; // Resume the game time
        }
    }
}
