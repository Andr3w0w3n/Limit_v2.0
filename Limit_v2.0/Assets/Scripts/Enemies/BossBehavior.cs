using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
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

    public Viewfinder rightView;
    public Viewfinder leftView;
    public Viewfinder groundView;
    public Viewfinder midView;

    public float movementSpeed = 5;
    public float fastMovementSpeed = 15;
    public float jumpForce = 10;
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

    private float attackWaitTimer = 0;

    private Vector2 movementVector;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTimer)
        {
            attackWaitTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isDrained)
        {

        }

        //wait to see if player stays in section long enough
        else if ((playerAtGround || playerAtMid || playerAtLeft || playerAtRight) && !isAttacking && !isStartingAttacking && !isBasicAttacking)
        {
            activeTimer = true;
            if(playerAtLeft || playerAtRight)
            {
                if (attackWaitTimer >= sideWaitTime)
                {
                    activeTimer = false;
                    attackWaitTimer = 0;
                    if (playerAtLeft)
                    {
                        isStartingAttacking = true;
                    }
                    else
                    {
                        isStartingAttacking = true;
                    }
                }
            }else if (playerAtMid)
            {
                if (attackWaitTimer >= groundWaitTime)
                {
                    activeTimer = false;
                    attackWaitTimer = 0;
                    isStartingAttacking = true;
                }
            }else if (playerAtGround)
            {
                if(attackWaitTimer >= midWaitTime)
                {
                    activeTimer = false;
                    attackWaitTimer = 0;
                    isStartingAttacking = true;
                }
            }
        }

        //starting attack stage
        else if (isStartingAttacking && !isBasicAttacking && !isAttacking)
        {
            if (playerAtLeft)
            {
                if(transform.position.x > attackLeftXPos + 1)
                {
                    moveLeft(true);
                }else if(transform.position.x < attackLeftXPos - 1)
                {
                    moveRight(true);
                }
                else
                {
                    isAttacking = true;
                    
                }
            }
            else if (playerAtRight)
            {
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
                }
            }else if (playerAtMid)
            {
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
                    isAttacking = true;
                }
            }
            else if (playerAtGround)
            {
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
                    isAttacking = true;
                }
            }
            else
            {
                print("Something is broken in the start attack section");
            }
        }

        //attacking Stage
        else if(!isStartingAttacking && !isBasicAttacking && isAttacking)
        {
            activeTimer = true;
            if (playerAtLeft)
            {
                if(attackWaitTimer >= sideWaitTime)
                {

                }
            }else if (playerAtRight)
            {
                if (attackWaitTimer >= sideWaitTime)
                {

                }
            }else if (playerAtMid)
            {
                if(attackWaitTimer >= midWaitTime)
                {

                }
            }else if (playerAtGround)
            {
                if(attackWaitTimer >= groundWaitTime)
                {

                }
            }
            else
            {
                print("Something is broken in attack");
            }
        }

        //AI stuff along with value updating
        else
        {
            // AI Stuff


            //Value Updating based off of map viewfinders
            if(groundView.playerInFrame && rightView.playerInFrame)
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

    }
    private void moveLeft(bool fastOrSlow)
    {

    }
}