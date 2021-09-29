using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    [SerializeField]
    private float maxVision = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckForObstacles(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckForObstacles(collision.gameObject);
        }
    }

    private void CheckForObstacles(GameObject collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position, maxVision);

        Debug.DrawLine(transform.position, collision.transform.position, Color.yellow);

        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider.gameObject.tag);

            if (hit.collider.gameObject.tag == "Player")
            {
                collision.gameObject.SetActive(false);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
