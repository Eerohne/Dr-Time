using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSystem : SystemInterface
{
    public bool isSideView = false; // True if side view level

    public bool isFree = true; // False if cutscene or pause
    public bool isAnimation; // True if cutscene

    public float speed = 5f; //Speed of the player
    public float life = 10; // Player's life
    public float scaleX; // Scale of the player

    ProjectileMotion launcher; //Reference to ProjectileManager
    FieldOfView fov; // Reference to the FieldOfView Script attached to the Player

    AudioManager soundManager; //Reference to the AudioManager

    public Canvas playerUI; //Reference to the UI

    public Item.ItemType selectedItem; // Current Selected Item from the inventory
    //[SerializeField] private UI_Inventory uiInventory;

    public Rigidbody2D rb; //Reference to the RigidBody2D
    private BoxCollider2D box2d;

    public static Inventory inventory = new Inventory(); //Player's Inventory
    
    public float immunityBetweenAttacks; // Upon Receiving Damage, time in seconds before receiving damage again

    public bool isImmune = false;
    
    float immunityBU; // Saved player immunity

    Renderer playerSprite; // Refenrence to Sprite Renderer

    public Color damageColor = Color.cyan;
    private Color defaultColor;

    public float jumpVelocity;

    // Sets up all default values
    void Awake()
    {
        //uiInventory.SetInventory(inventory); //passing inventory object into UIscript
        //Debug.Log(inventory.ToString());
        scaleX = transform.localScale.x;

        direction = new Vector2(0f, -1f);

        fov = GetComponent<FieldOfView>();
        launcher = FindObjectOfType<ProjectileMotion>();
        soundManager = FindObjectOfType<AudioManager>();
        playerSprite = GetComponent<SpriteRenderer>();
        box2d = GetComponent<BoxCollider2D>();

        defaultColor = playerSprite.material.color;

        immunityBU = immunityBetweenAttacks;
    }

    Vector2 movement; // Movement Vector of the Player
    [SerializeField]Vector2 direction; //Direction at which the player is pointing

    public Animator anim; //Reference to Animator attached to Player

    //bool jump = false; // True if Player is jumping
    //bool canJump = true; // True if Player can jump again
    [SerializeField] LayerMask floorMask;

    private void Start()
    {
        inventory = SaveSystem.LoadPlayer().inventory;
        if (inventory == null)
            inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseScript.GameIsPaused && !GameOverScript.gameOver) // When game is not paused
        {
            /*if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("Recorded");
                //playerUI.GetComponentInChildren<Health>().ChangeHealth(life--);
                gameObject.SendMessage("Damage", 1);
            }*/
            if (life <= 0) // If Player has no life left
            {
                GameOver();
                life = 10;
            }
            else if(isImmune)
            {
                immunityBetweenAttacks -= Time.deltaTime;
                if(immunityBetweenAttacks <= 0.01f)
                {
                    immunityBetweenAttacks = immunityBU;
                    isImmune = false;
                    playerSprite.material.color = defaultColor;
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) // On pressing E, the selected item is used
            {
                if(SceneManager.GetActiveScene().name != "Lobby")
                    Use();
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha1)) // On pressing 1, selects HealthPotion
            {
                selectedItem = Item.ItemType.HealthPotion;
                Debug.Log("Health Potion Selected!");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // On pressing 2, selects DamagePotion
            {
                selectedItem = Item.ItemType.DamagePotion;
                Debug.Log("Damage Potion Selected!");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // On pressing 3, selects SuperPotion
            {
                selectedItem = Item.ItemType.SuperPotion;
                Debug.Log("Super Potion Selected!");
            }

            if (isAnimation) return; // If currently in a Cutscene, stops the Update method for this frame
            else if (isFree)
            {
                if (!isSideView) // If Current Level is in a top-down mode
                {
                    //Movement Recording
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");

                    //Character Flip
                    Vector3 characterScale = transform.localScale;
                    if (movement.x < 0) characterScale.x = -scaleX;
                    else characterScale.x = scaleX;
                    transform.localScale = characterScale;

                    // Character Animation System
                    anim.SetFloat("Horizontal", movement.x);
                    anim.SetFloat("Vertical", movement.y);
                    anim.SetFloat("Speed", movement.sqrMagnitude);

                    float axis = Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical"));
                    
                    if (!isWalking)
                    {
                        if (axis > 0f)
                        {
                            PlayWalkingSound(false);
                            isWalking = true;
                        }
                    }
                    if (axis <= 0.01f)
                    {
                        PlayWalkingSound(true);
                        isWalking = false;
                    }
                }
                else //if side view
                {
                    if (IsGrounded() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
                    {
                        rb.velocity = Vector2.up * jumpVelocity;
                    }


                    movement.x = Input.GetAxisRaw("Horizontal");
                    rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

                    //Character Flip
                    Vector3 characterScale = transform.localScale;
                    if (movement.x < 0) characterScale.x = -scaleX;
                    else characterScale.x = scaleX;
                    transform.localScale = characterScale;

                    float axis = Mathf.Abs(movement.x);

                    // Character Animation System
                    if (axis > 0.1f || rb.velocity.y > 0)
                        anim.SetFloat("Horizontal", movement.x);
                    anim.SetFloat("Speed", movement.sqrMagnitude);

                    if (!isWalking)
                    {
                        if (axis > 0f && IsGrounded())
                        {
                            PlayWalkingSound(false);
                            isWalking = true;
                        }
                    }
                    if (axis <= 0.01f || !IsGrounded())
                    {
                        PlayWalkingSound(true);
                        isWalking = false;
                    }

                    //SideViewMovement();
                    /*//Movement Recording
                    movement.x = Input.GetAxisRaw("Horizontal");
                    if (Input.GetKeyDown(KeyCode.W) && jump == false)
                    {
                        jumpStart = transform.position;
                        jump = true;
                        canJump = false;
                    }

                    if (jump)
                        Jump();*/



                    // Implement SideView Walking Sound
                }

                // Save Pointing Direction
                if (!(Mathf.Abs(movement.x) <= 0.01f && Mathf.Abs(movement.y) <= 0.01f))
                {
                    direction = movement;
                }
                else
                {
                    Idle();
                }
            }
            else // Idle Player
            {
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", 0f);
                anim.SetFloat("Speed", 0f);

                Idle();
            }
        }

        if (GameOverScript.gameOver || PauseScript.GameIsPaused)
        {
            isWalking = false;
            PlayWalkingSound(true);
        }

        /*if (GameOverScript.gameOver)
        {
            foreach(Item item in inventory.itemList)
            {
                if (item.name == "Fragment" || item.name == "Key")
                    inventory.itemList.Remove(item);
            }
        }*/
    }

    void FixedUpdate()
    {
        // Character Displacement
        if(isFree && !isSideView)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, .1f, floorMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    /*void SideViewMovement()
    {
        
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(floorMask.value))
        {
            Debug.Log("Landed");
            jump = false;
            canJump = true;
        }
    }

    Vector2 jumpStart;
    public float jumpSpeed = 5f;
    public float maxJumpHeight = 10f;

    // Jump Function : Verifies if player should go up or down
    void Jump()
    {
        if ((transform.position.y - jumpStart.y) <= maxJumpHeight)
            movement.y = 1f;
        else
        {
            jump = false;
            movement.y = 0f;
        }
            
    }*/

    // Plays when Player is not moving
    void Idle()
    {
        Vector3 characterScale = transform.localScale;

        anim.SetFloat("DirectionX", direction.x);
        anim.SetFloat("DirectionY", direction.y);

        if (direction.x < 0) characterScale.x = -scaleX;
        else characterScale.x = scaleX;
        transform.localScale = characterScale;
    }

    // Called when Player receives damage
    void Damage(int amount)
    {
        if (!isImmune)
        {
            if (this.life > 0)
            {
                isImmune = true;
                playerSprite.material.color = damageColor;
                this.life -= amount;
                soundManager.Play("PlayerDamage");
                playerUI.GetComponentInChildren<Health>().ChangeHealth(life);
            }
        }
    }

    // Called when Player collects an item
    void Collect(Item item)
    {
        inventory.AddItem(item);
        PlayCollectionSound(item.itemType);
        Debug.Log(inventory.ToString());
    }

    // Plays the appropriate Sound from AudioManager
    void PlayCollectionSound(Item.ItemType itemType)
    {
        if (itemType == Item.ItemType.Coins)
        {
            soundManager.PlayIndividualSound("CoinCollection");
        }
        else
        {
            soundManager.PlayIndividualSound("ItemCollection");
        }
    }

    // Called when player has lost the game (no life or timer ends)
    void GameOver()
    {
        //soundManager.Play("GameOver");
        movement = Vector2.zero;
        PlayWalkingSound(true);
        GameOverScript.gameOver = true;
        Debug.Log("Game Over");
    }

    // Changes the IsFree boolean. 
    public void SetFree(bool _isFree)
    {
        if(_isFree == false)
        {
            movement = Vector2.zero;
        }

        isFree = _isFree;
    }

    // Returns the current pointing direction of the Player
    public override Vector2 GetDirection()
    {
        return direction;
    }

    // Called when Potion is used. Launches the appropriate effect
    void Use()
    {
        Item item = inventory.itemList[(int)selectedItem];

        if (item.amount > 0)
        {
            //Debug.Log(item.ToString() + " Used");

            switch (selectedItem)
            {
                case Item.ItemType.HealthPotion:
                    if (!(life == 10))
                    {
                        item.amount--;
                        this.life += item.effect;
                        if (life > 10)
                            life = 10;
                        playerUI.GetComponentInChildren<Health>().ChangeHealth(life);
                        soundManager.Play("DrinkPotion");
                    }
                    break;
                case Item.ItemType.DamagePotion:
                    item.amount--;
                    launcher.Throw(transform.position, false);
                    break;
                case Item.ItemType.SuperPotion:
                    item.amount--;
                    launcher.Throw(transform.position, true);
                    break;
            }
        }
    }

    // Returns a target position for a thrown potion
    /*Vector2 SelectTargetPosition()
    {
        if (fov.visibleEnemies.Count != 0) // If enemies in the vicinity of Player: Select Random Enemy
        {
            int randomIndex = Mathf.RoundToInt(Random.Range(0F, fov.visibleEnemies.Count - 1));

            return fov.visibleEnemies[randomIndex].position;
        }
        else // Return a position staight ahead
        {
            return Vector2.zero;
            /*Vector2 targetPosition = transform.position;

            targetPosition.x += direction.x * launcher.projectileRange;
            targetPosition.y += direction.y * launcher.projectileRange;

            return targetPosition;
        }
    }*/

    // Return current life of Player
    public float GetLife()
    {
        return this.life;
    }

    // Returns Current velocity of player
    public Vector2 GetVelocity()
    {
        return movement * speed;
    }

    public Vector2 GetMovement()
    {
        return movement;
    }

    public FieldOfView GetFOV()
    {
        return fov;
    }

    public enum WalkingSurface { Grass, Stone } // Enum of the walking surfaces
    public WalkingSurface walkingSurface; // Main Surface on which the player walks in the level
    bool isWalking;

    // Plays the appropriate walking sound
    public void PlayWalkingSound(bool pause)
    {
        switch (walkingSurface)
        {
            case WalkingSurface.Grass:
                if(!pause)
                    soundManager.Play("GrassWalking");
                else
                    soundManager.Stop("GrassWalking");
                break;
            case WalkingSurface.Stone:
                if (!pause)
                    soundManager.Play("StoneWalking");
                else
                    soundManager.Stop("StoneWalking");
                break;
        }
    }
}
