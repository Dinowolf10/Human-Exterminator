using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private PauseManager pauseManager;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private SpriteRenderer playerSprite;

    [SerializeField]
    private SpriteRenderer spookZoneSprite;

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
    public AudioClip spookSound;
    GameObject[] walls;
    public PhaseBar phaseBar;
    float timeAfterPhase;
    float timeAfterSpook;
    float spookCooldown = 3;
    float timeSinceSpook = 3;

    private bool isSpooking = false;
    public bool isDying = false;

    [SerializeField]
    private float fade = 1.0f;

    [SerializeField]
    private float fadeSpeed = 0.3f;

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
        // Return if the player isDying
        if (isDying)
        {
            return;
        }

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
            // Return if the player isDying
            if (isDying)
            {
                return;
            }

            if (Input.GetKey(KeyCode.Space))
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


            if (Input.GetKeyDown(KeyCode.Q) && timeSinceSpook > spookCooldown)
            {
                StartCoroutine("Boo");
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

            if (Input.GetKeyUp(KeyCode.Space) || phaseBar.slider.value <= 0)
            {
                col.enabled = true;
                timeAfterPhase = timeAfterPhase + Time.deltaTime;
            }           
            else if (Input.GetKey(KeyCode.Space))
            {
                if (col.enabled == false)
                {
                    return;
                }
                col.enabled = false;

            }
            else if(phaseBar.slider.value == phaseBar.slider.minValue)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Scares the human if they are turned away from the ghost and within range of the spook. 
    /// </summary>
    IEnumerator Boo()
    {
        Vector3 playPos = transform.position;
        Quaternion direction = Quaternion.Euler(0, 0, 0);
        timeSinceSpook = 0;

        // Plays spook audio
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = spookSound;
        audio.Play();

        // Sets isSpooking to true
        isSpooking = true;
        playerAnimator.SetBool("isSpooking", true);
        playerAnimator.SetBool("isDown", false);
        playerAnimator.SetBool("isUp", false);
        playerAnimator.SetBool("isRight", false);
        playerAnimator.SetBool("isLeft", false);

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

        // Waits for 1 second
        yield return new WaitForSeconds(1f);

        // Sets isSpooking to false and isDown to true
        isSpooking = false;
        playerAnimator.SetBool("isDown", true);
        playerAnimator.SetBool("isUp", false);
        playerAnimator.SetBool("isRight", false);
        playerAnimator.SetBool("isLeft", false);
        playerAnimator.SetBool("isSpooking", false);
    }

    /// <summary>
    /// Plays player "death animation" and restarts level
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerDeath()
    {
        // Sets isDying to true
        isDying = true;
        playerAnimator.SetBool("isDown", true);
        playerAnimator.SetBool("isUp", false);
        playerAnimator.SetBool("isRight", false);
        playerAnimator.SetBool("isLeft", false);
        playerAnimator.SetBool("isSpooking", false);

        // Hides spookZoneSprite
        spookZoneSprite.enabled = false;

        // Sets fade to 1
        fade = 1.0f;
        
        // Loops while fade is greater than 0
        while (fade > -0.2f)
        {
            // Gets reference to playerSprite's color
            Color playerSpriteColor = playerSprite.color;

            // Sets the alpha to fade variable
            playerSpriteColor.a = fade;

            // Sets the playerSprite to playerSpriteColor with updated alpha
            playerSprite.color = playerSpriteColor;

            // Increases fade by fadeSpeed variable
            fade -= fadeSpeed;

            // Waits for 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }

        // After player has faded away, wait a second before restarting the level
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Find the direction of the player and change it
    /// </summary>
    void FindDirection()
    {
        // Upright
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !isSpooking)
        {
            playDir = Direction.UpRight;
            playerAnimator.SetBool("isUp", true);
            playerAnimator.SetBool("isDown", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Downright
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !isSpooking)
        {
            playDir = Direction.DownRight;
            playerAnimator.SetBool("isDown", true);
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Downleft
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !isSpooking)
        {
            playDir = Direction.DownLeft;
            playerAnimator.SetBool("isDown", true);
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Upleft
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            playDir = Direction.UpLeft;
            playerAnimator.SetBool("isUp", true);
            playerAnimator.SetBool("isDown", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Up
        else if ((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)) || (Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.W))) 
        { 
            playDir = Direction.Up;
            playerAnimator.SetBool("isUp", true);
            playerAnimator.SetBool("isDown", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Right
        else if ((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A)) || (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D))) 
        { 
            playDir = Direction.Right;
            playerAnimator.SetBool("isRight", true);
            playerAnimator.SetBool("isDown", false);
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Down
        else if ((Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (Input.GetKeyUp(KeyCode.W) && Input.GetKey(KeyCode.S))) 
        { 
            playDir = Direction.Down;
            playerAnimator.SetBool("isDown", true);
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isLeft", false);
        }
        // Left
        else if ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A))) 
        { 
            playDir = Direction.Left;
            playerAnimator.SetBool("isLeft", true);
            playerAnimator.SetBool("isDown", false);
            playerAnimator.SetBool("isRight", false);
            playerAnimator.SetBool("isUp", false);
        }
    }
}
