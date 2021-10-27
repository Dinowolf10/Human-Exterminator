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

    [SerializeField]
    private FadeToBlack fadeToBlack;

    [SerializeField]
    bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        // Gets index of current scene
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Stores reference to FadeToBlack script component
        fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<FadeToBlack>();

        // Checks for null reference
        if (fadeToBlack == null)
        {
            Debug.LogError("fadeToBlack is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If there are no more enemies left in the current level, start fade transition
        if (enemies.Count == 0 && !isFading)
        {
            fadeToBlack.StartCoroutine("FadeScreen");
            isFading = true;
        }
    }

    /// <summary>
    /// Loads the next level
    /// </summary>
    public void ChangeLevel()
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
