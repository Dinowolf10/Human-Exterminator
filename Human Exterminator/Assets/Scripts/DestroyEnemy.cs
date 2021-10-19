using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;

    // Start is called before the first frame update
    void Start()
    {
        // Stores reference to levelChanger script from the LevelManager game object
        levelChanger = GameObject.Find("LevelManager").GetComponent<LevelChanger>();

        // Checks if levelChanger is null
        if (levelChanger == null)
        {
            // If it is, print an error message
            Debug.LogError("levelChanger is null!");
        }
    }

    /// <summary>
    /// Removes this enemy from the levelChanger enemies list
    /// </summary>
    private void OnDestroy()
    {
        // Removes this enemy from the levelChanger enemies list
        levelChanger.enemies.Remove(this.gameObject);
    }
}
