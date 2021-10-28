using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an entity around a path going towards one point at a time
public class WalkPath : MonoBehaviour
{
    private int nextPoint = 0; // the index of the next point to get to
    private float waitTime = 0; // tracks how much the entity has waited

    public int waitTimeAtPivots = 1; // the amount of time the entity pauses when they get to a point
    public float walkSpeed = 1f; // how fast this entity will move along its path
    public List<Vector3> path = new List<Vector3>(); // Add points to this list to create the path. Order matters

    [SerializeField]
    private GameObject vision;

    [SerializeField]
    private EnemyAnimationsManager animationManager;

    private bool isWalking = false;

    public bool isDying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Gets reference to enemyVisionContainer child game object
        vision = transform.GetChild(0).gameObject;

        // Gets reference to animationManager script
        animationManager = gameObject.GetComponent<EnemyAnimationsManager>();

        // Checks for null references
        if (vision == null)
        {
            Debug.LogError("vision is null!");
        }
        if (animationManager == null)
        {
            Debug.LogError("enemy animationManager is null!");
        }

        // Calls animationManager's UpdateAnimationVariables method, passing in the angle and isWalking bool
        animationManager.UpdateAnimationVariables(vision.transform.eulerAngles.z, isWalking);
    }

    // Moves the entity towards the next point in the path. Once they pass it, they move to the next point
    void Update()
    {
        // If there is no path/points to walk to or this enemy is dying, return
        if(path.Count <= 0 || isDying) {
            return;
        }

        // wait at a point, sets isWalking to false
        if(waitTime > 0) {
            isWalking = false;
            waitTime -= Time.deltaTime;

            // Calls animationManager's UpdateAnimationVariables method, passing in the angle and isWalking bool
            animationManager.UpdateAnimationVariables(vision.transform.eulerAngles.z, isWalking);

            return;
        }

        // calculate direction to move
        Vector3 target = path[nextPoint];
        Vector3 direction = (target - transform.position).normalized;

        // Calculates angle to rotate enemyVisionContainer to using direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotates this enemyVisionContainer towards the next point in the path
        vision.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move towards next path point
        transform.position = Vector2.MoveTowards(transform.position, path[nextPoint], walkSpeed * Time.deltaTime);

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
        else
        {
            // Sets isWalking to true
            isWalking = true;

            // Calls animationManager's UpdateAnimationVariables method, passing in the angle and isWalking bool
            animationManager.UpdateAnimationVariables(angle, isWalking);
        }
    }
}
