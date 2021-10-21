using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private PauseManager pauseManager;

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
    float timeAfterPhase;
    float timeAfterSpook;
    float spookCooldown = 1;
    float timeSinceSpook = 0;

    Direction playDir = Direction.Up;

    private void Start()
    {
        //adds all wall tagged items to the wall list
        walls = GameObject.FindGameObjectsWithTag("Wall");

        // Store a reference to the pauseManager script component from the levelManager game object
        pauseManager = GameObject.Find("LevelManager").GetComponent<PauseManager>();

        // Checks if pauseManager is null
        if (pauseManager == null)
        {
            // If it is, print an error message
            Debug.LogError("pauseManager is null!");
        }
    }

    private void FixedUpdate()
    {
        //takes the input of the x and y axis
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //modifies player movement based off of input and time
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
        movement *= Time.deltaTime;

        //moves the player sprite
        transform.Translate(movement);
    }

    // Update is called once per frame
    void Update()
    {
        //phases the player through walls if they have enough phase
        Phase();

        // Checks if game is not paused
        if (!pauseManager.gamePaused)
        {
            //decreases the phase bar while holding down left shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                phaseBar.slider.value -= Time.deltaTime;
                timeAfterPhase = 0;
            }
            else
            {
                timeAfterPhase += Time.deltaTime;
            }

            if (timeAfterPhase >= 3.0)
            {
                phaseBar.slider.value += (Time.deltaTime / 2);
            }

            //keep track of player facing direction 
            FindDirection();


            if (Input.GetKeyDown(KeyCode.Space) && timeSinceSpook > spookCooldown)
            {
                Boo();
            }
            else
            {
                timeSinceSpook += Time.deltaTime;
            }

        }
    }

    /// <summary>
    /// gets the collider of each item in the wall array, and when the left shift button is pressed they will be disabled
    /// </summary>
    void Phase()
    {
        foreach (GameObject wall in walls)
        {
            Collider2D col = wall.GetComponent<Collider2D>();

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                col.enabled = true;
                timeAfterPhase = timeAfterPhase + Time.deltaTime;
            }           
            else if (Input.GetKey(KeyCode.LeftShift) && phaseBar.slider.value > 0)
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
        timeSinceSpook = 0;

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
        spook.SetActive(true);
        //spook.enabled = true;
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
