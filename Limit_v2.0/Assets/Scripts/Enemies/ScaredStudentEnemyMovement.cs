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
    private Animator anim;
    private bool isMoving = true;
    private float xPositionChange;
    private Transform trans;

    //infected globals
    public GameObject followDetection;
    public GameObject attackDetection;
    public GameObject attackVinesObj;
    private Vector2 followDetecPos;
    private Vector2 attackDetecPos;
    private bool spawnViewfinders = false;
    private GameObject followV;
    private GameObject followA;
    private GameObject attackVines;
    public float infectWaitTime = 1;
    public float infectFinishAnimTime = 10;
    private bool follow;
    private bool attack;
    private Transform playerPos;
    private Viewfinder followViewfinderScript;
    private Viewfinder attackViewfinderScript;
    public float attackChargeTime = 1;
    private float attackFunctionsTimer = 0;
    public float attackWaitTime = .5f;
    public float actualAttackTime = 4;
    private bool executeAttack = false;
    private bool chargeCompleted = false;

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
        if(followDetection == null|| attackDetection == null)
        {
            Debug.Log("There is no component for the player detection system");
        }
        playerPos = GameController.instance.playerPos;
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
                spawnViewfinders = true;
            }
        }
        else
        {
            timeSpeed = GameController.instance.speed;

            //check to see if the enemy is infected or not
            if (isInfected)
            {
                infectedUpdate();
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
            Destroy(followV);
            Destroy(followA);
            Object.Destroy(gameObject);
        }
    }

    private float TimeChange()
    {
        return Random.Range(minTimeSwitch, maxTimeSwitch);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Toxic_Slime" && !isInfected)
        {
            countDown = true;
        }
    }

    private bool randomBool()
    {
        return ((int)Random.Range(0, 100)) % 2 == 0;
    }

    private void infectedUpdate()
    {
        playerPos = GameController.instance.playerPos;
        //spawn viewfinders if they are not spawned yet
        if (spawnViewfinders)
        {
            followV = Instantiate(followDetection, transform.position, transform.rotation);
            followA = Instantiate(attackDetection, transform.position, transform.rotation);
            followViewfinderScript = followV.GetComponent<Viewfinder>();
            attackViewfinderScript = followA.GetComponent<Viewfinder>();
            spawnViewfinders = false;
        }

        //update viewfinder position
        followV.transform.position = new Vector2(transform.position.x, transform.position.y + 4.2f);
        followA.transform.position = new Vector2(transform.position.x, transform.position.y + 3.1f);

        //update script values
        if(followViewfinderScript.playerInFrame == true)
        {
            follow = true;
        }
        else
        {
            follow = false;
        }

        if(attackViewfinderScript.playerInFrame == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
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
        if (chargeCompleted)
        {
            if (!executeAttack)
            {
                attackFunctionsTimer += Time.fixedDeltaTime;
                if (attackFunctionsTimer >= attackWaitTime)
                {
                    executeAttack = true;
                    anim.SetBool("Attack", true);
                    attackVines = Instantiate(attackVinesObj, new Vector3(transform.position.x+.2f, transform.position.y + 2.6f, transform.position.z), transform.rotation);
                }
            }
            else
            {    
                attackFunctionsTimer += Time.fixedDeltaTime;
                if (attackFunctionsTimer >= actualAttackTime)
                {
                    anim.SetBool("Attack", false);
                    executeAttack = false;
                    chargeCompleted = false;
                    attackFunctionsTimer = 0;
                    Destroy(attackVines);
                }
            }

        }
        else if (attack)
        {
            anim.SetBool("Charge", true);
           
            attackFunctionsTimer += Time.fixedDeltaTime;
            if(attackFunctionsTimer >= attackChargeTime)
            {
                chargeCompleted = true;
                attackFunctionsTimer = 0;
            }
        }
        else if(follow)
        {
            anim.SetBool("Charge", false);
            attackFunctionsTimer = 0;

            if (playerPos.position.x < transform.position.x)
            {
                movementVec = new Vector2(-runningSpeed / 2, 0);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                movementVec = new Vector2(runningSpeed / 2, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb2d.velocity = movementVec * timeSpeed;
            print(playerPos.ToString());
        }
        else
        {
            anim.SetBool("Charge", false);
            attackFunctionsTimer = 0;

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
}