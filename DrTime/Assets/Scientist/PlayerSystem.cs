using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public static Inventory inventory = new Inventory(); //Player's Inventory

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
    }

    Vector2 movement; // Movement Vector of the Player
    [SerializeField]Vector2 direction; //Direction at which the player is pointing

    public Animator anim; //Reference to Animator attached to Player

    bool jump = false; // True if Player is jumping
    bool canJump = true; // True if Player can jump again
    [SerializeField] LayerMask floorMask;

    // Update is called once per frame
    void Update()
    {
        if (!PauseScript.GameIsPaused) // When game is not paused
        {
            if(life <= 0) // If Player has no life left
            {
                GameOver(); 
            }

            if (Input.GetKeyDown(KeyCode.E)) // On pressing E, the selected item is used
            {
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
                else //if side view
                {
                    //Movement Recording
                    movement.x = Input.GetAxisRaw("Horizontal");
                    if (Input.GetKeyDown(KeyCode.W) && jump == false)
                    {
                        jumpStart = transform.position;
                        jump = true;
                        canJump = false;
                    } 

                    if (jump)
                        Jump();
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
    }

    void FixedUpdate()
    {
        // Character Displacement
        if(isFree)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            
    }

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
        if (this.life > 0)
        {
            this.life -= amount;
            //GetComponent<AudioManager>().Play("PlayerDamage");
            soundManager.Play("PlayerDamage");
            playerUI.GetComponentInChildren<Health>().ChangeHealth(life);
        }
    }

    // Called when Player collects an item
    void Collect(Item item)
    {
        inventory.AddItem(item);
        //PlayCollectionSound(item.itemType);
        Debug.Log(inventory.ToString());
    }

    // Plays the appropriate Sound from AudioManager
    void PlayCollectionSound(Item.ItemType itemType)
    {
        if (itemType == Item.ItemType.Coins)
        {
            soundManager.Play("CoinCollection");
        }
        else
        {
            soundManager.Play("ItemCollection");
        }
    }

    // Called when player has lost the game (no life or timer ends)
    void GameOver()
    {
        soundManager.Play("GameOver");
        PauseScript.GameIsPaused = true;
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
            Debug.Log(item.ToString() + " Used");

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
                    launcher.Throw(transform.position, SelectTargetPosition(), false);
                    break;
                case Item.ItemType.SuperPotion:
                    item.amount--;
                    launcher.Throw(transform.position, SelectTargetPosition(), true);
                    break;
            }
        }
    }

    // Returns a target position for a thrown potion
    Vector2 SelectTargetPosition()
    {
        if (fov.visibleEnemies.Count != 0) // If enemies in the vicinity of Player: Select Random Enemy
        {
            int randomIndex = Mathf.RoundToInt(Random.Range(0F, fov.visibleEnemies.Count - 1));

            return fov.visibleEnemies[randomIndex].position;
        }
        else // Return a position staight ahead
        {
            Vector2 targetPosition = transform.position;

            targetPosition.x += direction.x * launcher.projectileRange;
            targetPosition.y += direction.y * launcher.projectileRange;

            return targetPosition;
        }
    }

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

}
