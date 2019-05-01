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
    private bool attackingMidOrGround = false;
    private bool activeTimer = false;
    private bool isAttacking = false;
    private bool hasInitiatedAttack = false;
    private bool isBasicAttacking = false;
    private bool isStartingAttacking = false;
    private bool isDrained = false;
    private bool playerHitBoss = false;
    private float sideAttackAnimRunTime = 126.0f/60.0f;
    private float chargeAttackAnimRunTime = 59.0f/60.0f;
    private Vector2 attackMovementVector;
    private float throwAttackRunTime = 95.0f / 60.0f;
    private float throwTimer = 0;
    private bool thrown = false;
    private bool haveHit = false;
    private bool leaveDown = false;
    private bool missedHit = false;

    //Major attack public variables
    public float sideWaitTime = 2;
    public float midWaitTime = 3;
    public float groundWaitTime = 5;
    public float attackRightXPos;
    public float attackLeftXPos;
    public float jumpForce = 10;
    public float drainedWaitTime = 5;
    public float midOrGroundAttackDuration = 5;
    public GameObject smallDebris;
    public float downTime = 10;
    public float playerHitTime = 134.0f / 60.0f;
    public float gettingUpTime = 59.0f / 60.0f;

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
    private float aITimer = 0;

    //AI and basic attack public variables
    public float movementSpeed = 5;
    public float fastMovementSpeed = 15;
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
    public float stamina = 100;
    public float health = 100;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        prevXPos = transform.position.x;
        attackMovementVector = new Vector2(movementSpeed * 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timeSpeed = GameController.instance.speed;
        if (activeTimer)
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //drained stage (after stamina drain)
        if (isDrained)
        {
            activeTimer = true;
            anim.SetBool("isDowned", true);
            gameObject.tag = "downBoss";
            if (playerHitBoss && !haveHit && !missedHit)
            {
                activeTimer = false;
                timer = 0;
                anim.SetTrigger("playerHit");
                health -= 25;
                stamina = 100;
                haveHit = true;
            }
            if(timer >= downTime && !haveHit && !missedHit)
            {
                anim.SetTrigger("timeUp");
                stamina = 100;
                missedHit = true;
            }
            if (haveHit && !missedHit)
            {
                activeTimer = true;
                if(timer >= playerHitTime)
                {
                    leaveDown = true;
                }
            }
            if (missedHit && !haveHit)
            {
                activeTimer = true;
                if (timer >= gettingUpTime)
                {
                    leaveDown = true;
                }
            }
            if (leaveDown)
            {
                activeTimer = false;
                timer = 0;
                leaveDown = false;
                print("did we go in here?");
                anim.SetBool("isDowned", false);
                isDrained = false;
                haveHit = false;
            }
        }

        //attacking Stage
        else if (isAttacking)
        {
            activeTimer = true;

            //if attacking is on one of the sides
            if ((playerAtRight || playerAtLeft) && hasInitiatedAttack)
            {
                if(timer >= throwTimer && !thrown)
                {
                    thrown = true;
                    if (playerAtLeft)
                    {
                        spawnDebris(false);
                    }
                    else
                    {
                        spawnDebris(true);
                    }
                }
                if (timer >= sideAttackAnimRunTime)
                {
                    anim.SetBool("AttackSide", false);
                    activeTimer = false;
                    timer = 0;
                    isAttacking = false;
                    thrown = false;
                    hasInitiatedAttack = false;
                    playerAtLeft = false;
                    playerAtRight = false;
                    playerAtMid = false;
                    playerAtGround = false;
                    if (stamina <= 0)
                    {
                        isDrained = true;
                        haveHit = false;
                    }
                }
            }

            //attacking mid
            else if(playerAtMid && hasInitiatedAttack)
            {
                if (attackingMidOrGround)
                {
                    attackingWithZoomies();
                    if (timer >= midOrGroundAttackDuration)
                    {
                        activeTimer = false;
                        timer = 0;
                        anim.SetTrigger("EndAttack");
                        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                        isAttacking = false;
                        hasInitiatedAttack = false;
                        playerAtLeft = false;
                        playerAtRight = false;
                        playerAtMid = false;
                        playerAtGround = false;
                        attackingMidOrGround = false;
                        if (stamina <= 0)
                        {
                            isDrained = true;
                            haveHit = false;
                        }
                    }
                }
                else if (timer >= chargeAttackAnimRunTime)
                {
                    anim.SetBool("Charge", false);
                    rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    transform.position = new Vector2(transform.position.x, 3);
                    attackingMidOrGround = true;
                    timer = 0;
                }
            }

            //attacking ground
            else if(playerAtGround && hasInitiatedAttack)
            {
                
                if (attackingMidOrGround)
                {
                    
                    attackingWithZoomies();
                    if(timer >= midOrGroundAttackDuration)
                    {
                        
                        activeTimer = false;
                        timer = 0;
                        anim.SetTrigger("EndAttack");
                        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                        isAttacking = false;
                        hasInitiatedAttack = false;
                        playerAtLeft = false;
                        playerAtRight = false;
                        playerAtMid = false;
                        playerAtGround = false;
                        attackingMidOrGround = false;
                        if (stamina <= 0)
                        {
                            isDrained = true;
                            haveHit = false;
                        }
                    }
                }
                else if (timer >= chargeAttackAnimRunTime)
                {
                    anim.SetBool("Charge", false);
                    attackingMidOrGround = true;
                    rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    timer = 0;
                }
            }
            else if (playerAtLeft)
            {
                anim.SetBool("AttackSide", true);
                rb2d.velocity = new Vector2(0, 0);
                transform.position = new Vector2(attackLeftXPos, transform.position.y);
                hasInitiatedAttack = true;
                stamina -= 10;
            }else if (playerAtRight)
            {
                anim.SetBool("AttackSide", true);
                rb2d.velocity = new Vector2(0, 0);
                transform.position = new Vector2(attackRightXPos, transform.position.y);
                hasInitiatedAttack = true;
                stamina -= 10;
            }else if(playerAtMid)
            {
                anim.SetBool("Charge", true);
                hasInitiatedAttack = true;
                stamina -= 25;
            }else if (playerAtGround)
            {
                anim.SetBool("Charge", true);
                hasInitiatedAttack = true;
                stamina -= 15;
            }
            else
            {

            }
        }

        //starting attack stage
        else if (isStartingAttacking)
        {
            //Check to see which position the player is at based off the boolean variables
            //move if not in the proper position
            //next stage if in proper position

            if (playerAtLeft)
            {
                if(transform.position.x < attackLeftXPos - .1)
                {
                    anim.SetBool("Sprint", true);
                    moveRight(true);
                }
                else if(transform.position.x > attackLeftXPos + .1)
                {
                    anim.SetBool("Sprint", true);
                    moveLeft(true);
                }
                else
                {
                    //transform.position = new Vector2(attackLeftXPos, 0);
                    isStartingAttacking = false;
                    isAttacking = true;
                    anim.SetBool("Sprint", false);
                }
            }else if (playerAtRight)
            {
                if (transform.position.x < attackRightXPos - .1)
                {
                    anim.SetBool("Sprint", true);
                    moveRight(true);
                }
                else if (transform.position.x > attackRightXPos + .1)
                {
                    anim.SetBool("Sprint", true);
                    moveLeft(true);
                }
                else
                {                  
                    isStartingAttacking = false;
                    isAttacking = true;
                    anim.SetBool("Sprint", false);
                }
            }
            else
            {
                print("Something is broken in the starting attack stage");
            }
        }

        //wait to see if player stays in section long enough
        else if ((playerAtGround || playerAtMid || playerAtLeft || playerAtRight) && !isAttacking && !isStartingAttacking && !isBasicAttacking)
        {
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
                if (timer >= midWaitTime)
                {
                    activeTimer = false;
                    timer = 0;
                    isAttacking = true;
                }
            }
            else if (playerAtGround)
            {
                if (timer >= groundWaitTime)
                {
                    activeTimer = false;
                    timer = 0;
                    isAttacking = true;
                }
            }
            aIMovement();
        }

        //AI stuff for when not main attacking along with value updating
        else
        {
            // AI Stuff
            aIMovement();

            //Value Updating based off of map viewfinders
            gameObject.tag = "Boss";
            if (groundView.playerInFrame && rightView.playerInFrame && midView)
            {
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }else if (groundView.playerInFrame && leftView.playerInFrame && midView)
            {
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }
            else if (groundView.playerInFrame && rightView.playerInFrame)
            {
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            }
            else if(groundView.playerInFrame && leftView.playerInFrame)
            {
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }
            else if(midView.playerInFrame && rightView.playerInFrame)
            {
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            }
            else if(midView.playerInFrame && leftView.playerInFrame)
            {
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }else if(midView.playerInFrame && groundView.playerInFrame)
            {
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            }
            else if (groundView.playerInFrame)
            {
                playerAtGround = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtMid = false;
            } else if (midView.playerInFrame)
            {
                playerAtMid = true;
                playerAtLeft = false;
                playerAtRight = false;
                playerAtGround = false;
            } else if (rightView.playerInFrame)
            {
                playerAtRight = true;
                playerAtMid = false;
                playerAtLeft = false;
                playerAtGround = false;
            } else if (leftView.playerInFrame)
            {
                playerAtLeft = true;
                playerAtMid = false;
                playerAtRight = false;
                playerAtGround = false;
            }
            else
            {
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

    //zooming back and forth
    private void attackingWithZoomies()
    {
        if (transform.position.x <= -6)
        {
            attackMovementVector = new Vector2(movementSpeed*3, 1);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x >= 6)
        {
            attackMovementVector = new Vector2(-movementSpeed*3,1);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rb2d.velocity = attackMovementVector * timeSpeed;
    }

    //spawning debris that is thrown
    //true is left, false is right
    private void spawnDebris(bool rightOrLeft)
    {
        if (rightOrLeft)
        {
            GameObject smallDeb = Instantiate(smallDebris, new Vector2(transform.position.x + 1, transform.position.y + 1), Quaternion.Euler(0,0, 45));
        }
        else
        {
            GameObject smallDeb = Instantiate(smallDebris, new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.Euler(0,0,-45));
        }   
    }

    //making the boss move (AI stuff)
    private void aIMovement()
    {

        //walking v idle animation
        if (prevXPos != transform.position.x)
        {
            prevXPos = transform.position.x;
            anim.SetBool("Walk", true);
        }
        else
        {
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
            aITimer += Time.fixedDeltaTime;
            if (!executeAttack)
            {
                if (aITimer >= basicAttackWaitTime * GameController.instance.attackSpeed)
                {
                    executeAttack = true;
                    //anim.SetBool("Attack", true);
                    attackVines = Instantiate(attackVinesObj, new Vector3(transform.position.x + .2f, transform.position.y + 2.6f, transform.position.z), transform.rotation);
                }
            }
            else
            {
                if (aITimer >= basicAttackTime * GameController.instance.attackSpeed)
                {
                    //anim.SetBool("Attack", false);
                    executeAttack = false;
                    chargeCompleted = false;
                    aITimer = 0;
                    Destroy(attackVines);
                    isBasicAttacking = false;
                }
            }

        }
        else if (basicAttack)
        {
            //anim.SetBool("BasicCharge", true);

            aITimer += Time.fixedDeltaTime;
            if (aITimer >= basicAttackChargeTime * GameController.instance.attackSpeed)
            {
                chargeCompleted = true;
                aITimer = 0;
            }
        }
        else
        {
            //anim.SetBool("BasicCharge", false);
            aITimer = 0;

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