using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an entity around a path going towards one point at a time
public class WalkPath : MonoBehaviour
{
    private int nextPoint = 0; // the index of the next point to get to
    private float waitTime = 0; // tracks how much the entity has waited

    public int waitTimeAtPivots = 0; // the amount of time the entity pauses when they get to a point
    public float walkSpeed = 10f; // how fast this entity will move along its path
    public List<Vector3> path = new List<Vector3>(); // Add points to this list to create the path. Order matters

    // Start is called before the first frame update
    void Start() {}

    // Moves the entity towards the next point in the path. Once they pass it, they move to the next point
    void Update()
    {
        if(path.Count <= 0) {
            return;
        }

        // wait at a point
        if(waitTime > 0) {
            waitTime -= Time.deltaTime;
            return;
        }

        // calculate direction to move
        Vector3 target = path[nextPoint];
        Vector3 direction = (target - transform.position).normalized;

        // Calculates angle to rotate to using direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotates this enemy towards the next point in the path
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move forward
        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);

        // check if target point was reached
        Vector3 directionToTarget = target - transform.position;
        if(Vector3.Dot(directionToTarget, direction) <= 0) {
            // correct position to be on the point
            transform.position = path[nextPoint];

            // start moving to the next point
            nextPoint++;
            if(nextPoint > path.Count - 1) {
                nextPoint = 0;
            }

            // wait the desired amount first
            waitTime = waitTimeAtPivots;
        }
    }
}
