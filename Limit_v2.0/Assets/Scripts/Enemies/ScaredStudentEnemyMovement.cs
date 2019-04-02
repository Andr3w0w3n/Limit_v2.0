using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredStudentEnemyMovement : MonoBehaviour
{
    public float minTimeSwitch;
    public float maxTimeSwitch;
    private float currentTime;
    private float runTime;
    private Vector2 movementVec;
    public float runningSpeed;
    private Rigidbody2D rb2d;
    private float timeSpeed;
    private bool isInfected = false;
    private bool setSpeed = false;
    private bool countDown = false;
    public float infectWaitTime = 1;
    public float infectFinishAnimTime = 10;
    private Animator anim;
    private bool isMoving = true;
    private float xPositionChange;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        runTime = TimeChange();
        movementVec = new Vector2(runningSpeed, 0);
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        xPositionChange = trans.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        currentTime += Time.fixedDeltaTime;
        if (countDown)
        {
            if(currentTime >= infectWaitTime)
            {
                movementVec = new Vector2(0, 0);
                anim.SetBool("Infected", true);
            }
            if(currentTime >= infectFinishAnimTime)
            {
                anim.SetBool("Infected", false);
                countDown = false;
                isInfected = true;
            }
        }
        else
        {
            timeSpeed = GameController.instance.speed;

            //check to see if the enemy is infected or not
            if (isInfected)
            {
                //set new speed of enemy
                if (!setSpeed)
                {
                    movementVec = new Vector2(runningSpeed / 2, 0);
                    setSpeed = !setSpeed;
                }

                //check to see if the enemy is moving
                if (trans.position.x != xPositionChange)
                {
                    anim.SetBool("isMoving", true);
                    xPositionChange = trans.position.x;
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
                
                //actual movement
                if (currentTime >= runTime)
                {
                    runTime = TimeChange();
                    currentTime = 0;
                    movementVec = -movementVec;
                }
                else
                {
                    if (movementVec.x / -1 < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    rb2d.velocity = movementVec * timeSpeed;
                } 
            }
            else
            {
                if (currentTime >= runTime)
                {
                    runTime = TimeChange();
                    currentTime = 0;
                    movementVec = -movementVec;
                }
                else
                {
                    if (movementVec.x / -1 < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    rb2d.velocity = movementVec * timeSpeed;
                }
            }
        }
        if (transform.position.y < 0f)
        {
            Object.Destroy(gameObject);
        }
    }

    private float TimeChange()
    {
        return Random.Range(minTimeSwitch, maxTimeSwitch);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Toxic_Slime")
        {
            countDown = true;
        }
    }

    private bool randomBool()
    {
        return ((int)Random.Range(0, 100)) % 2 == 0;
    }
}