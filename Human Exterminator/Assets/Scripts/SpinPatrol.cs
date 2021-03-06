using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make an enemy spin in circles. They can be made to stop and wait at certain angles
public class SpinPatrol : MonoBehaviour
{
    private float waitTimeLeft = 0f;
    private int nextImportantAngle; // the next angle to potentially pause at

    public float angle = 0; // degrees representing the direction the enemy is facing
    public float spinSpeed = 0.25f; // full rotations per second. Note: actual spin duration will be more if pause spots are utilized
    public float waitTime = 1f; // amount of time to wait at each pause spot in seconds
    public bool clockwise = true;
    public bool oscillate = false; // if true, makes the enemy spin back and forth between two set angles

    // whether or not to stop at certain angles, only cardinals and diagonals
    public bool pauseLeft = false;
    public bool pauseUp = false;
    public bool pauseRight = false;
    public bool pauseDown = false;
    public bool pauseUpLeft = false;
    public bool pauseDownLeft = false;
    public bool pauseUpRight = false;
    public bool pauseDownRight = false;

    // Reference grabbed in editor
    [SerializeField]
    private EnemyAnimationsManager animationsManager;

    // Start is called before the first frame update
    void Start()
    {
        // Stores reference to this enemy's EnemyAnimationsManager script
        animationsManager = transform.GetComponentInParent<EnemyAnimationsManager>();

        // Checks for null reference
        if (animationsManager == null)
        {
            Debug.LogError("animationsManager is null!");
        }

        // make enemy start facing the input angle
        while(angle >= 360) {
            angle -= 360;
        }
        while(angle < 0) {
            angle += 360;
        }
        SetRotationToAngle();

        // calculate first important angle based on start
        nextImportantAngle = (int)angle / 45 * 45; // round down to nearest multiple of 45
        if(!clockwise && angle % 45 != 0) {
            nextImportantAngle += 45;
        }

        if(nextImportantAngle < 0) {
            nextImportantAngle += 360;
        }
        if(nextImportantAngle > 360) {
            nextImportantAngle -= 360;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTimeLeft > 0) {
            waitTimeLeft -= Time.deltaTime;
        } else {
            // spin more
            bool changeDirection = false;

            if(!clockwise) {
                angle += 360 * spinSpeed * Time.deltaTime;
            
                // check if passed the next important angle
                if(angle >= nextImportantAngle) {
                    // check if supposed to pause at this angle
                    if(IsPauseAngle(nextImportantAngle)) {
                        angle = nextImportantAngle;
                        waitTimeLeft = waitTime;

                        if(oscillate) {
                            changeDirection = true;
                        }
                    }

                    // set the next target angle
                    nextImportantAngle += 45;
                    if(nextImportantAngle > 360) {
                        // basically, convert 405 to 45
                        nextImportantAngle -= 360;
                    }
                }
            } else { // clockwise
                angle -= 360 * spinSpeed * Time.deltaTime;

                // check if passed the next important angle
                if(angle <= nextImportantAngle) {
                    // check if supposed to pause at this angle
                    if(IsPauseAngle(nextImportantAngle)) {
                        angle = nextImportantAngle;
                        waitTimeLeft = waitTime;

                        if(oscillate) {
                            changeDirection = true;
                        }
                    }

                    // set the next target angle
                    nextImportantAngle -= 45;
                    if(nextImportantAngle < 0) {
                        // basically, convert -45 to 305
                        nextImportantAngle += 360;
                    }
                }
            }

            if(changeDirection) {
                clockwise = !clockwise;

                // move next angle
                if(clockwise) {
                    nextImportantAngle -= 90;
                    if(nextImportantAngle < 0) {
                        nextImportantAngle += 360;
                    }
                } else {
                    nextImportantAngle += 90;
                    if(nextImportantAngle > 360) {
                        nextImportantAngle -= 360;
                    }
                }
            }
            
            // keep angle between 0 and 360, inclusive
            while(angle >= 360) {
                angle -= 360;
            }
            while(angle < 0) {
                angle += 360;
            }
            SetRotationToAngle();
        }
    }

    // updates the enemy's rotation to match the target angle
    private void SetRotationToAngle() {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Updates enemy animation state
        animationsManager.UpdateAnimationVariables(angle, false);
    }

    // checks the direction boolean that matches the input angle
    private bool IsPauseAngle(int angle) {
        switch(angle) {
            default:
                return false;
            case 0:
            case 360:
                return pauseRight;
            case 45:
                return pauseUpRight;
            case 90:
                return pauseUp;
            case 135:
                return pauseUpLeft;
            case 180:
                return pauseLeft;
            case 225:
               return pauseDownLeft;
            case 270:
                return pauseDown;
            case 315:
                return pauseDownRight;
        }
    }
}
