using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //class globals
    private Rigidbody2D rb2d;
    public float horizontalForce = 10;
    public float verticalForce = 10;
    private Animator animate;
    private Transform tr;
    private float position;
    private BoxCollider2D boxCol;
    private bool isOnGround;
    private float moveInput;
    public float damageMovement = 0.5f;
    public float userSprintSpeed = 5;
    private float sprintSpeed = 0;

    private float lastYPos;
    private float lastXPos;

    private bool isInAir;
    private bool isFalling = false;

    public Ghost ghost;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        position = transform.position.x;

        lastYPos = transform.position.y;
        lastXPos = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get movementInput
        moveInput = Input.GetAxis("Horizontal");

        if (GameController.instance.timeSlowActive && (transform.position.x != lastXPos || transform.position.y != lastYPos))
        {
            ghost.makeGhost = true;
        }
        else
        {
            ghost.makeGhost = false;
        }
        
        if (!(animate.GetCurrentAnimatorStateInfo(0).IsName("Damage")))
        {
            if (isInAir)
            {
               
                if (lastYPos > transform.position.y)
                {
                    isInAir = false;
                    animate.SetBool("isJumping", false);
                    isFalling = true;
                    animate.SetBool("isFalling", true);
                }
            }
            if (isFalling)
            {
                if((((float)((int)(lastYPos*100)))/100) == (((float)((int)(transform.position.y*100))/100)))
                {
                    isFalling = false;
                    animate.SetBool("isFalling", false);
                }
            }
            //check to see if sprinting
            if (Input.GetKey(KeyCode.LeftShift) && GameController.instance.stanimaValue > 0)
            {
                sprintSpeed = userSprintSpeed*moveInput;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    GameController.instance.stanimaValue -= .1f;
                }              
            }
            else
            {
                if(GameController.instance.stanimaValue < 25)
                {
                    GameController.instance.stanimaValue += .03f;
                }
                sprintSpeed = 0;
            }
            
            //movement code
            float positionTwo = transform.position.x;
            Vector2 jump = new Vector2(0, verticalForce);
            rb2d.velocity = new Vector2(moveInput * horizontalForce+sprintSpeed, rb2d.velocity.y);

            //swap character look position and animator change
            
            if (Input.GetKey(KeyCode.D))
            {
                animate.SetBool("isRunning", true);
                tr.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animate.SetBool("isRunning", true);
                tr.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                animate.SetBool("isRunning", false);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rb2d.AddForce(jump);
                isInAir = true;
                animate.SetBool("isJumping", true);
                isOnGround = false;
            }
        }
        else
        {
            //move the character slightly when damaged
            rb2d.velocity = new Vector2(damageMovement/tr.localScale.x*-1, 0);
        }

        if(transform.position.x != lastXPos || transform.position.y != lastYPos)
        {
            lastYPos = transform.position.y;
            lastXPos = transform.position.x;
        }


    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
        if(collision.gameObject. tag == "Locker" && transform.position.y > 2 )
        {
            isOnGround = true;
        }
    }
}