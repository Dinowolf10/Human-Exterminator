using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private int currentLevelIndex;

    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // If there are no more enemies left in the current level, change levels
        if (enemies.Count == 0)
        {
            ChangeLevel();
        }
    }

    /// <summary>
    /// Loads the next level
    /// </summary>
    private void ChangeLevel()
    {
        // Checks if there is another scene after the current scene
        // If there is, load the next scene
        if (currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Loads the next scene
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
        // Otherwise print an error message
        else
        {
            Debug.LogError("There is no next scene");
        }
    }
}
