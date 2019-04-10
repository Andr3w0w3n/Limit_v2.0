using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveInterestBehavior : MonoBehaviour
{
    private bool playerIn = false;
    public Viewfinder viewfinderScript;
    public float runningForce;
    public float sprintSpeed;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        if(viewfinderScript == null)
        {
            Debug.Log("You have no script attached");
        }
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
    }

    private void FixedUpdate()
    {
        if (playerIn)
        {
            rb2d.velocity = new Vector2(moveInput * horizontalForce + sprintSpeed, rb2d.velocity.y);
        }
    }
}
