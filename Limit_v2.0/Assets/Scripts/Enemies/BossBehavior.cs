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
    private bool activeTimer = false;
    private bool isAttacking = false;
    private bool isInPosition = false;
    private bool isBasicAttacking = false;
    private bool isStartingAttacking = false;
    private bool attack = false;
    private bool isDrained = false;
    private bool playerHitBoss = false;

    //Major attack public variables
    public float sideWaitTime = 2;
    public float midWaitTime = 1;
    public float groundWaitTime = 5;
    public float attackRightXPos;
    public float attackLeftXPos;
    public float attackGroundXPos;
    public float attackMidXPos;
    public float stamina = 100;
    public float groundAnimationAttackWaitTime = 3;
    public float midAnimationAttackWaitTime = 3;
    public float sideAnimationAttackWaitTime = 3;
    public float drainedWaitTime = 5;
    public float drainedReboundTime = 3;

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
            print("oh no im drained");
            activeTimer = true;
            if (drainedRebound)
            {
                if (timer >= drainedReboundTime)
                {
                    print("im rebounding for drainage");
                    drainedRebound = false;
                    activeTimer = false;
                    timer = 0;
                    isDrained = false;
                }
            }
            else if (timer >= drainedWaitTime)
            {
                drainedRebound = true;
                stamina = 100;
                timer = 0;
            }
            else if (playerHitBoss)
            {

            }
            else
            {
                print("something is broken in drained");
            }
        }

        //attacking Stage
        else if (!isStartingAttacking && !isBasicAttacking && isAttacking)
        {
            print("now im attacking");
            activeTimer = true;
            if (isInPosition)
            {
                //play animation and wait until animation is over, once animation is over, then
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                {
                    isAttacking = false;
                }
                if (stamina <= 0)
                {
                    isDrained = true;
                }
            }
            else if (playerAtLeft)
            {
                if (timer >= sideWaitTime)
                {
                    isInPosition = true;
                    isAttacking = false;
                    activeTimer = false;
                    timer = 0;
                    stamina -= 10;
                }
            }
            else if (playerAtRight)
            {
                if (timer >= sideWaitTime)
                {
                    isInPosition = true;
                    isAttacking = false;
                    activeTimer = false;
                    timer = 0;
                    stamina -= 10;
                }
            }
            else if (playerAtMid)
            {
                if (timer >= midWaitTime)
                {
                    isInPosition = true;
                    isAttacking = false;
                    activeTimer = false;
                    timer = 0;
                    stamina -= 25;
                }
            }
            else if (playerAtGround)
            {
                if (timer >= groundWaitTime)
                {
                    isInPosition = true;
                    isAttacking = false;
                    activeTimer = false;
                    timer = 0;
                    stamina -= 15;
                }
            }
            else
            {
                print("Something is broken in attack");
            }
        }

        //starting attack stage
        else if (isStartingAttacking && !isBasicAttacking && !isAttacking)
        {
            print("now im starting to attack because you are in my swamp");
            if (playerAtLeft)
            {
                print("am i making it here??");
                if (transform.position.x > attackLeftXPos + 1)
                {
                    moveLeft(true);
                }
                else if (transform.position.x < attackLeftXPos - 1)
                {
                    moveRight(true);
                }
                else
                {
                    isAttacking = true;
                    movementVector = new Vector2(0, 0);
                    isStartingAttacking = false;
                }
            }
            else if (playerAtRight)
            {
                print("what about here??");
                if (transform.position.x > attackRightXPos + 1)
                {
                    moveLeft(true);
                }
                else if (transform.position.x < attackRightXPos - 1)
                {
                    moveRight(true);
                }
                else
                {
                    isAttacking = true;
                    movementVector = new Vector2(0, 0);
                    isStartingAttacking = false;
                }
            }
            else if (playerAtMid)
            {
                anim.SetBool("Sprint", true);
                print("here??");
                if (transform.position.x > attackMidXPos + 1)
                {
                    moveLeft(true);
                }
                else if (transform.position.x < attackMidXPos - 1)
                {
                    moveRight(true);
                }
                else
                {
                    anim.SetBool("Sprint", false);
                    anim.SetBool("AttackSide", true);
                    isAttacking = true;
                    movementVector = new Vector2(0, 0);
                    isStartingAttacking = false;
                }
            }
            else if (playerAtGround) //may make this so it does not have a designated position
            {
                anim.SetBool("Sprint", true);
                print("how about here??");
                if (transform.position.x > attackGroundXPos + 1)
                {
                    moveLeft(true);
                }
                else if (transform.position.x < attackGroundXPos - 1)
                {
                    moveRight(true);
                }
                else
                {
                    anim.SetBool("Sprint", false);
                    anim.SetBool("Charge", true);
                    movementVector = new Vector2(0, 0);
                    isAttacking = true;
                    isStartingAttacking = false;
                }
            }
            else
            {
                print("Something is broken in the start attack section");
            }
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
            /*if (groundView.playerInFrame && rightView.playerInFrame && midView)
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
        */
        }
        prevXPos = transform.position.x;
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