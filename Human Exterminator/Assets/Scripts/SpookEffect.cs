using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookEffect : MonoBehaviour
{
    private float speed = 2;
    private Vector3 pos;
    float duration = .5f;
    float timeAlive = 0;

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            timeAlive += Time.deltaTime;

            if (timeAlive > duration)
            {
                //this.enabled = false;
                this.gameObject.SetActive(false);
                timeAlive = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        // Checks if its hitting an enemy
        if (collide.gameObject.tag == "Enemy")
        {
            // Calls this enemies death coroutine if the enemy is not already dying
            if (!collide.gameObject.GetComponent<WalkPath>().isDying)
            {
                // Sets isDying to true from WalkPath script
                collide.gameObject.GetComponent<WalkPath>().isDying = true;

                // Starts enemy death coroutine from EnemyAnimationsManager script
                collide.gameObject.GetComponent<EnemyAnimationsManager>().StartCoroutine("EnemyDeath");
            }
        }
    }
}
