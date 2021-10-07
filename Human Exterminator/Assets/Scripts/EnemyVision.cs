using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    [SerializeField]
    private float maxVision = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the colliding object is the player
        // If it is the player, call the CheckForObstacles method
        if (collision.gameObject.tag == "Player")
        {
            // Disable player
            collision.gameObject.SetActive(false);

            // Restart the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Checks for obstacles in between enemy and player
            //CheckForObstacles(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Checks if the colliding object is the player
        // If it is the player, call the CheckForObstacles method
        if (collision.gameObject.tag == "Player")
        {
            // Disable player
            collision.gameObject.SetActive(false);

            // Restart the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Checks for obstacles in between enemy and player
            //CheckForObstacles(collision.gameObject);
        }
        
    }

    /// <summary>
    /// Checks for obstacles in between the enemy and player
    /// </summary>
    /// <param name="collision"></param>
    private void CheckForObstacles(GameObject collision)
    {
        // Shoots a raycast from this enemy's position to the player's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position, maxVision);

        // Draws the raycast as a debug line
        Debug.DrawLine(transform.position, collision.transform.position, Color.yellow);

        // If there was a collider hit
        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider.gameObject.tag);

            // Check if the collider hit was the player
            // If it was the player, disable the player and restart the current scene
            if (hit.collider.gameObject.tag == "Player")
            {
                // Disable player
                collision.gameObject.SetActive(false);

                // Restart the current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
