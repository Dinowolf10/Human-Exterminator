using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject ControlsButton;

    [SerializeField]
    private GameObject BackButton;

    [SerializeField]
    private GameObject ControlsText;

    private void Start()
    {
        // Hides back button and controls text
        BackButton.SetActive(false);
        ControlsText.SetActive(false);
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void PlayGame()
    {
        // Loads the first level
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Displays the controls to the user
    /// </summary>
    public void DisplayControls()
    {
        // Hides play and controls buttons
        playButton.SetActive(false);
        ControlsButton.SetActive(false);

        // Displays back button and controls text
        BackButton.SetActive(true);
        ControlsText.SetActive(true);
    }

    /// <summary>
    /// Brings user back to the main menu
    /// </summary>
    public void BackToMenu()
    {
        // Displays play and controls buttons
        playButton.SetActive(true);
        ControlsButton.SetActive(true);

        // Hides back button and controls text
        BackButton.SetActive(false);
        ControlsText.SetActive(false);
    }
}
