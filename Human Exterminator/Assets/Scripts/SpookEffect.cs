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
        float temp;
        //timeAlive += Time.deltaTime;

        //movement based on direction and speed and duration
        //switch ((int)transform.rotation.eulerAngles.z)
        //{
        //    case 0:
        //        pos = transform.position;
        //        pos.y += speed * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 45:
        //        pos = transform.position;
        //        temp = speed * (float)Math.Cos(45); 
        //        pos.y += temp * Time.deltaTime;
        //        pos.x += temp * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 90:
        //        pos = transform.position;
        //        pos.x += speed * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 135:
        //        pos = transform.position;
        //        temp = speed * (float)Math.Cos(45);
        //        pos.y -= temp * Time.deltaTime;
        //        pos.x += temp * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 180:
        //    case -180:
        //        pos = transform.position;
        //        pos.y -= speed * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case -135:
        //    case 224:
        //        pos = transform.position;
        //        temp = speed * (float)Math.Cos(45);
        //        pos.y -= temp * Time.deltaTime;
        //        pos.x -= temp * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 270:
        //        pos = transform.position;
        //        pos.x -= speed * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    case 315:
        //        pos = transform.position;
        //        temp = speed * (float)Math.Cos(45);
        //        pos.y += temp * Time.deltaTime;
        //        pos.x -= temp * Time.deltaTime;
        //        transform.position = pos;
        //        break;
        //    default:
        //         break;
        //}        

    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        // Checks if its hitting an enemy
        if (collide.gameObject.tag == "Enemy")
        {
            // Checks for obstacles in between enemy and player
            Destroy(collide.gameObject);
        }
    }
}
