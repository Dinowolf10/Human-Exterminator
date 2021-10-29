using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    private static Music instance = null; // make sure only one instance

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject); // keep music playing when restarting and changing levels
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // destroy this if leaving the play state (either main menu or victory screen)
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 0 || currentScene == SceneManager.sceneCountInBuildSettings - 1) {
            Destroy(this.gameObject);
        }
    }
}
