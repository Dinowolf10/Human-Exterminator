using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    enum Direction
    {
        Up, 
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft
    }

    public Vector2 speed = new Vector2(10, 10);
    public GameObject spook;
    GameObject[] walls;
    public PhaseBar phaseBar;

    Direction playDir = Direction.Up;

    private void Start()
    {
        //adds all wall tagged items to the wall list
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        //takes the input of the x and y axis
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //modifies player movement based off of input and time
        Vector3 movement = new Vector3(speed.x*inputX, speed.y*inputY, 0);
        movement *= Time.deltaTime;

        //moves the player sprite
        transform.Translate(movement);

        //phases the player through walls if they have enough phase
        Phase();
        

        //decreases the phase bar while holding down control
        if (Input.GetKey(KeyCode.LeftControl))
        {
            phaseBar.slider.value = phaseBar.slider.value - Time.deltaTime;
        }

        //keep track of player facing direction 
        FindDirection();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Boo();
        }


    }

    /// <summary>
    /// gets the collider of each item in the wall array, and when the left control button is pressed they will be disabled
    /// </summary>
    void Phase()
    {
        foreach (GameObject wall in walls)
        {
            Collider2D col = wall.GetComponent<Collider2D>();

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                col.enabled = true;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && phaseBar.slider.value > 0)
            {
                if (col.enabled == false)
                {
                    return;
                }
                col.enabled = false;
            }
        }
    }


    /// <summary>
    /// Scares the human if they are turned away from the ghost and within range of the spook. 
    /// </summary>
    void Boo()
    {
        Vector3 playPos = transform.position;
        Quaternion direction = Quaternion.Euler(0, 0, 0);

        //sets the direction of the spook off of play orientation
        switch (playDir)
        {
            case Direction.Up:
                break;
            case Direction.UpRight:
                direction = Quaternion.Euler(0, 0, 45);
                break;
            case Direction.Right:
                direction = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.DownRight:
                direction = Quaternion.Euler(0, 0, 135);
                break;
            case Direction.Down:
                direction = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.DownLeft:
                direction = Quaternion.Euler(0, 0, 225);
                break;
            case Direction.Left:
                direction = Quaternion.Euler(0, 0, 270);
                break;
            case Direction.UpLeft:
                direction = Quaternion.Euler(0, 0, 315);
                break;
        }
        Instantiate(spook, playPos, direction);
    }

    /// <summary>
    /// Find the direction of the player and change it
    /// </summary>
    void FindDirection()
    {
        if (!Input.anyKeyDown) { }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) { playDir = Direction.Up; }//up
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) { playDir = Direction.UpRight; }//upRight
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) { playDir = Direction.Right; }//right
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) { playDir = Direction.DownRight; }//downRight
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) { playDir = Direction.Down; }//down
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) { playDir = Direction.DownLeft; }//downLeft
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) { playDir = Direction.Left; } //left
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) { playDir = Direction.UpLeft; }//upLeft
        else { return; }
    }
}
