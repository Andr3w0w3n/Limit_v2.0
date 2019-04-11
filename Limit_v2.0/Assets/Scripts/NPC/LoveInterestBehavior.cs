using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveInterestBehavior : MonoBehaviour
{
    private bool playerIn = false;
    public Viewfinder viewfinderScript;
    public float runningForce;
    private float sprintSpeedInternal;
    public float sprintSpeed = 5;
    private Rigidbody2D rb2d;
    public float horizontalForce = 10;
    private int directionInput = 1;
    private float prevXPos;

    // Start is called before the first frame update
    void Start()
    {
        if(viewfinderScript == null)
        {
            Debug.Log("You have no script attached");
        }
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(viewfinderScript.playerInFrame == true)
        {
            playerIn = true;
        }
        else
        { 
            playerIn = false;
        }
        if (!playerIn && (transform.position.x > GameController.instance.playerPos.position.x+20 || transform.position.x < GameController.instance.playerPos.position.x - 20))
        {
            sprintSpeedInternal = sprintSpeed;
        }
        else
        {
            sprintSpeedInternal = 0;
        }
    }

    private void FixedUpdate()
    {
        
        if (!playerIn)
        {
            directionInput = getPlayerDirection();
            rb2d.velocity = new Vector2(directionInput * horizontalForce + sprintSpeedInternal, 0);
            transform.localScale = new Vector3(directionInput, 1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        if(prevXPos != transform.position.x)
        {
            //movement animation
        }
        else
        {
            //static animation
        }
    }

    private int getPlayerDirection()
    {
        if(transform.position.x > GameController.instance.playerPos.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
