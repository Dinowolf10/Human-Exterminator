using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsManager : MonoBehaviour
{
    // Reference grabbed in editor
    [SerializeField]
    private Animator enemyAnimator;

    /// <summary>
    /// Updates enemy animator variables based on passed in angle and bool
    /// </summary>
    /// <param name="angle"></param>
    public void UpdateAnimationVariables(float angle, bool isWalking)
    {
        // If this enemy is not walking
        if (!isWalking)
        {
            // Set all the walking variables to false
            enemyAnimator.SetBool("isWalkingRight", false);
            enemyAnimator.SetBool("isWalkingLeft", false);
            enemyAnimator.SetBool("isWalkingUp", false);
            enemyAnimator.SetBool("isWalkingDown", false);

            // If this enemy is facing up, set isIdleUp to true and the other of the idle bools to false
            if ((angle >= 45 && angle <= 135) || (angle <= -225 && angle >= -315))
            {
                enemyAnimator.SetBool("isIdleUp", true);
                enemyAnimator.SetBool("isIdleRight", false);
                enemyAnimator.SetBool("isIdleLeft", false);
                enemyAnimator.SetBool("isIdleDown", false);
            }
            // If this enemy is facing left, set isIdleLeft to true and the other of the idle bools to false
            else if ((angle >= 135 && angle <= 225) || (angle <= -135 && angle >= -225))
            {
                enemyAnimator.SetBool("isIdleLeft", true);
                enemyAnimator.SetBool("isIdleRight", false);
                enemyAnimator.SetBool("isIdleUp", false);
                enemyAnimator.SetBool("isIdleDown", false);
            }
            // If this enemy is facing down, set isIdleDown to true and the other of the idle bools to false
            else if ((angle >= 225 && angle <= 315) || (angle <= -45 && angle >= -135))
            {
                enemyAnimator.SetBool("isIdleDown", true);
                enemyAnimator.SetBool("isIdleRight", false);
                enemyAnimator.SetBool("isIdleLeft", false);
                enemyAnimator.SetBool("isIdleUp", false);
            }
            // If this enemy is facing right, set isIdleRight to true and the rest of the idle bools to false
            else if ((angle >= 315 || angle <= 45) || (angle <= -315 || angle >= -45))
            {
                enemyAnimator.SetBool("isIdleRight", true);
                enemyAnimator.SetBool("isIdleLeft", false);
                enemyAnimator.SetBool("isIdleUp", false);
                enemyAnimator.SetBool("isIdleDown", false);
            }
        }
        // If this enemy is walking
        else if (isWalking)
        {
            // Set all the idle variables to false
            enemyAnimator.SetBool("isIdleRight", false);
            enemyAnimator.SetBool("isIdleLeft", false);
            enemyAnimator.SetBool("isIdleUp", false);
            enemyAnimator.SetBool("isIdleDown", false);

            // If this enemy is facing up, set isWalkingUp to true and the rest of the walking bools to false
            if ((angle >= 45 && angle <= 135) || (angle <= -225 && angle >= -315))
            {
                enemyAnimator.SetBool("isWalkingUp", true);
                enemyAnimator.SetBool("isWalkingRight", false);
                enemyAnimator.SetBool("isWalkingLeft", false);
                enemyAnimator.SetBool("isWalkingDown", false);
            }
            // If this enemy is facing left, set isWalkingLeft to true and the rest of the walking bools to false
            else if ((angle >= 135 && angle <= 225) || (angle <= -135 && angle >= -225))
            {
                enemyAnimator.SetBool("isWalkingLeft", true);
                enemyAnimator.SetBool("isWalkingRight", false);
                enemyAnimator.SetBool("isWalkingUp", false);
                enemyAnimator.SetBool("isWalkingDown", false);
            }
            // If this enemy is facing down, set isWalkingDown to true and the rest of the walking bools to false
            else if ((angle >= 225 && angle <= 315) || (angle <= -45 && angle >= -135))
            {
                enemyAnimator.SetBool("isWalkingDown", true);
                enemyAnimator.SetBool("isWalkingRight", false);
                enemyAnimator.SetBool("isWalkingLeft", false);
                enemyAnimator.SetBool("isWalkingUp", false);
            }
            // If this enemy is facing right, set isWalkingRight to true and the rest of the walking bools to false
            else if ((angle >= 315 || angle <= 45) || (angle <= -315 || angle >= -45))
            {
                enemyAnimator.SetBool("isWalkingRight", true);
                enemyAnimator.SetBool("isWalkingLeft", false);
                enemyAnimator.SetBool("isWalkingUp", false);
                enemyAnimator.SetBool("isWalkingDown", false);
            }
        }
    }
}
