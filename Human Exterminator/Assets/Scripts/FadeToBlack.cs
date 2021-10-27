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

    public IEnumerator FadeScreen()
    {
        fade = 0.0f;

        while (fade < 1.5f)
        {
            Color imageObjectColor = imageObject.color;
            imageObjectColor.a = fade;
            imageObject.color = imageObjectColor;
            fade += fadeSpeed;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2.0f);

        levelChanger.ChangeLevel();
    }
}
