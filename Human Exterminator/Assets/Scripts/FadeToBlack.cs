using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    private Image imageObject;

    [SerializeField]
    private LevelChanger levelChanger;

    [SerializeField]
    private float fade = 0.0f;

    [SerializeField]
    private float fadeSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        // Stores reference to FadeToBlack image component
        imageObject = GameObject.Find("FadeToBlack").GetComponent<Image>();

        // Stores reference to LevelManager's levelChanger script
        levelChanger = GameObject.Find("LevelManager").GetComponent<LevelChanger>();

        // Checks for null references
        if (imageObject == null)
        {
            Debug.LogError("imageObject is null!");
        }
        if (levelChanger == null)
        {
            Debug.LogError("levelChanger is null!");
        }
    }

    /// <summary>
    /// Fades screen before level change
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeScreen()
    {
        // Sets fade to 0
        fade = 0.0f;

        // Loops while fade is less than 1.5f
        while (fade < 1.5f)
        {
            // Gets reference to imageObject's color
            Color imageObjectColor = imageObject.color;

            // Sets the alpha to fade variable
            imageObjectColor.a = fade;

            // Sets the imageObject to imageObjectColor with updated alpha
            imageObject.color = imageObjectColor;

            // Increases fade by fadeSpeed variable
            fade += fadeSpeed;

            // Waits for 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }

        // After screen has faded to black, wait a couple seconds
        yield return new WaitForSeconds(2.0f);

        // Change Levels
        levelChanger.ChangeLevel();
    }
}
