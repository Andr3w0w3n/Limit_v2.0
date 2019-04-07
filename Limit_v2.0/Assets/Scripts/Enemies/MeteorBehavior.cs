using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    public float minXMeteorSpeed = 5;
    public float maxXMeteorSpeed = 10;
    public float minYMeteorSpeed = 5;
    public float maxYMeteorSpeed = 10;
    public float deleteYPosition = -10;
    public float deleteXPositionOne = -15;
    public float deleteXPositionTwo = 150;
    private Rigidbody2D rigidBody2Dee;
    private Vector2 movementVec;
    private float hypot;

    public Sprite meteorSprite;
    public GameObject toxic;

    //old exclimation variables
    /*
    public GameObject exlimation;
    private bool activeWarning = false;
    public ExclimationBehavior warningScript;
    private GameObject exlimationObject;
    */

    // Start is called before the first frame update
    void Start()
    {
        movementVec = new Vector2(Random.Range(minXMeteorSpeed, maxXMeteorSpeed), Random.Range(minYMeteorSpeed, maxYMeteorSpeed)*-1);
        rigidBody2Dee = GetComponent<Rigidbody2D>();
        
        if(movementVec.x > 0)
        {
            rigidBody2Dee.MoveRotation(-90 - rigidBody2Dee.rotation + (trigMaths(movementVec.x, movementVec.y)));
        }
        else
        {
            rigidBody2Dee.MoveRotation(-90 - rigidBody2Dee.rotation +(360 - (trigMaths(movementVec.x, movementVec.y))));
        }
        
        if(meteorSprite == null)
        {
            Debug.Log("there is no added sprite");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= deleteYPosition || transform.position.x <= deleteXPositionOne || transform.position.x >= deleteXPositionTwo)
        {
            //old destroy function for exclimation stuff
            //Destroy(exlimationObject);

            Object.Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        rigidBody2Dee.velocity = movementVec*GameController.instance.speed;

        //old meteor detection stuff
        /*
        if (transform.position.x <= GameController.instance.cameraPos.position.x + 10 && !activeWarning) 
        {
            exlimationObject = Instantiate(exlimation, transform.position, Quaternion.identity);
            activeWarning = true;
        }else if (activeWarning)
        {
            warningScript.meteorXPos = transform.position.x;
            warningScript.meteorYPos = transform.position.y;
        }*/
    }
    private float trigMaths(float x, float y)
    {
        hypot = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        if (x < 0)
        {
            x = -x;
        }
        if (y < 0)
        {
            y = -y;
        }
        float thetaOne = (180/Mathf.PI) * (Mathf.Asin(y / hypot));
        return 90 - thetaOne;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameController.instance.lifeValue -= hypot*2;
        }
        if(GetComponent<SpriteRenderer>().sprite == meteorSprite && collision.gameObject.tag == "Ground")
        {
            createToxic();
            Destroy(this.gameObject, 1f);
        }
    }

    private void createToxic()
    {
        Vector2 position = new Vector2(transform.position.x, -0.55f);
        
        Instantiate(toxic, position, Quaternion.identity);
    }
}
