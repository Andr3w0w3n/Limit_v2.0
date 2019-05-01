using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //major attack private variables
    private bool playerAtRight = false;
    private bool playerAtLeft = false;
    private bool playerAtGround = false;
    private bool playerAtMid = false;

    /*may not need these variables
    private bool attackRight = false;
    private bool attackLeft = false;
    private bool attackMid = false;
    private bool attackGround = false;
    */

    private bool activeTimer = false;
    private bool isAttacking = false;
    private bool isInPosition = false;
    private bool isBasicAttacking = false;
    private bool isStartingAttacking = false;
    private bool isDrained = false;
    private bool playerHitBoss = false;

    //Major attack public variables
    public float sideWaitTime = 2;
    public float midWaitTime = 3;
    public float groundWaitTime = 5;
    public float attackRightXPos;
    public float attackLeftXPos;
    public float attackGroundXPos;
    public float attackMidXPos;
    public float stamina = 100;
    public float drainedWaitTime = 5;

    //major attack viewfinders
    public Viewfinder rightView;
    public Viewfinder leftView;
    public Viewfinder groundView;
    public Viewfinder midView;

    //AI private variables & basic attack variables
    private bool drainedRebound = false;
    private bool chargeCompleted= false;
    private bool executeAttack = false;
    private bool follow;
    private bool basicAttack = false;
    private GameObject attackVines;
    private float timeSpeed;
    private float prevXPos;

    //AI and basic attack public variables
    public float movementSpeed = 5;
    public float fastMovementSpeed = 15;
    public float jumpForce = 10;
    public Viewfinder basicAttackViewfinderScript;
    public float basicAttackWaitTime = .5f;
    public float basicAttackChargeTime = 1;
    public GameObject attackVinesObj;
    public float basicAttackTime = 4;

    //timer variables
    private float timer = 0;

    //self referencing variables
    private Vector2 movementVector;
    private Animator anim;
    private Rigidbody2D rb2d;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        prevXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpeed = GameController.instance.speed;
        if (activeTimer)
        {
            timer += Time.deltaTime;
        }
        //print("Timer says: " + timer);
    }

    private void FixedUpdate()
    {
        //drained stage (after stamina drain)
        if (isDrained)
        {
            
        }

        //attacking Stage
        else if (isAttacking)
        {
            
        }

        //starting attack stage
        else if (isStartingAttacking)
        {
            //Check to see which position the player is at based off the boolean variables
            if()
        }

        //wait to see if player stays in section long enough
        else if ((playerAtGround || playerAtMid || playerAtLeft || playerAtRight) && !isAttacking && !isStartingAttacking && !isBasicAttacking)
        {
            print("Get out of my swamp");
            activeTimer = true;
            if (playerAtLeft || playerAtRight)
            {
                if (timer >= sideWaitTime)
                {
                    activeTimer = false;
                    timer = 0;
                    isStartingAttacking = true;
                }
            }
            else if (playerAtMid)
            {
                if (timer >= groundWaitTime)
                {
                    activeTimer = false;
                    timer = 0;
                    isStartingAttacking = true;
                }
            }
            else if (playerAtGround)
            {
                if (timer >= midWaitTime)
                {
                    activeTimer = false;
                    timer = 0;
                    isStartingAttacking = true;
                }
            }
        }

        //AI stuff for when not main attacking along with value updating
        else
        {
            // AI Stuff
            aIMovement();

            //Value Updating based off of map viewfinders
            if (groundView.playerInFrame && rightView.playerInFrame && midView)
            {
                print("i be seeing where u are, ur at ground");
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }else if (groundView.playerInFrame && leftView.playerInFrame && midView)
            {
                print("i be seeing where u are, ur at ground");
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }
            else if (groundView.playerInFrame && rightView.playerInFrame)
            {
                print("i be seeing where u are, ur at right");
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            }
            else if(groundView.playerInFrame && leftView.playerInFrame)
            {
                print("i be seeing where u are, ur at left");
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }
            else if(midView.playerInFrame && rightView.playerInFrame)
            {
                print("i be seeing where u are, ur at right");
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            }
            else if(midView.playerInFrame && leftView.playerInFrame)
            {
                print("i be seeing where u are, ur at left");
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }else if(midView.playerInFrame && groundView.playerInFrame)
            {
                print("i be seeing where u are, ur at ground");
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }
            else if (groundView.playerInFrame)
            {
                print("i be seeing where u are, ur at ground");
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            } else if (midView.playerInFrame)
            {
                print("i be seeing where u are, ur at mid");
                playerAtMid = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtGround = false;
            } else if (rightView.playerInFrame)
            {
                print("i be seeing where u are, ur at right");
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            } else if (leftView.playerInFrame)
            {
                print("i be seeing where u are, ur at left");
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }
            else
            {
                print("nani?");
                playerAtGround = false;
                playerAtMid = false;
                playerAtRight = false;
                playerAtLeft = false;
            }
        }
    }

    //true means fast, false means slow
    private void moveRight(bool fastOrSlow)
    {
        if (fastOrSlow)
        {
            movementVector = new Vector2(movementSpeed * 2, 0);
        }
        else
        {
            movementVector = new Vector2(movementSpeed / 2, 0);
        }
        transform.localScale = new Vector3(1, 1, 1);
        rb2d.velocity = movementVector * timeSpeed;
    }
    private void moveLeft(bool fastOrSlow)
    {
        if (fastOrSlow)
        {
            movementVector = new Vector2(-movementSpeed * 2, 0);
        }
        else
        {
            movementVector = new Vector2(-movementSpeed / 2, 0);
        }
        transform.localScale = new Vector3(-1, 1, 1);
        rb2d.velocity = movementVector * timeSpeed;
    }

    //making the boss move
    private void aIMovement()
    {
        print("vroom vroom");
        print("pre x pos: " + prevXPos);
        print("x pos: " + transform.position.x);
        if((prevXPos < transform.position.x+0.5f) && (prevXPos > transform.position.x-0.5f))
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            print("made it out of walk");
            anim.SetBool("Walk", false);
        }
        prevXPos = transform.position.x;
        if (basicAttackViewfinderScript.playerInFrame == true)
        {
            basicAttack = true;
        }
        else
        {
            basicAttack = false;
        }
        if (chargeCompleted)
        {
            activeTimer = true;
            if (!executeAttack)
            {
                if (timer >= basicAttackWaitTime * GameController.instance.attackSpeed)
                {
                    executeAttack = true;
                    //anim.SetBool("Attack", true);
                    attackVines = Instantiate(attackVinesObj, new Vector3(transform.position.x + .2f, transform.position.y + 2.6f, transform.position.z), transform.rotation);
                }
            }
            else
            {
                if (timer >= basicAttackTime * GameController.instance.attackSpeed)
                {
                    //anim.SetBool("Attack", false);
                    executeAttack = false;
                    chargeCompleted = false;
                    timer = 0;
                    Destroy(attackVines);
                }
            }

        }
        else if (basicAttack)
        {
            print("poke poke");
            //anim.SetBool("BasicCharge", true);

            timer += Time.fixedDeltaTime;
            if (timer >= basicAttackChargeTime * GameController.instance.attackSpeed)
            {
                chargeCompleted = true;
                timer = 0;
            }
        }
        else
        {
            //anim.SetBool("BasicCharge", false);
            timer = 0;

            if (GameController.instance.playerPos.position.x < transform.position.x)
            {
                moveLeft(false);
            }
            else
            {
                moveRight(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isDrained)
        {
            playerHitBoss = true;
        }
    }
}