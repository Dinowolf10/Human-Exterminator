using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool gamePaused;

    [SerializeField]
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Starts game
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        // If the user presses the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is not paused, pause the game
            if (!gamePaused)
            {
                PauseGame();
            }
            // Otherwise the game is already paused so resume the game
            else
            {
                ResumeGame();
            }
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        // Enables pause menu
        pauseMenu.SetActive(true);

        // Sets time scale to zero
        Time.timeScale = 0;

        // Sets gamePaused to true
        gamePaused = true;
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void ResumeGame()
    {
        // Disables pause menu
        pauseMenu.SetActive(false);

        // Sets time scale to one
        Time.timeScale = 1;

        // Sets gamePaused to false
        gamePaused = false;
    }

    /// <summary>
    /// Returns user to main menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        // Sets time scale to one
        Time.timeScale = 1;

        // Loads the first scene (main menu)
        SceneManager.LoadScene(0);
    }
}
