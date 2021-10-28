using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
 
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
