using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclimationBehavior : MonoBehaviour
{
    //this is all the old version of exclimation behavior
    /*
    private float xPos;
    private float yPos;
    public float meteorXPos;
    public float meteorYPos;
    private float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (meteorYPos < GameController.instance.cameraPos.position.y)
        {
            Object.Destroy(gameObject);
        }
        transformPos();
        transform.position = new Vector3(xPos, yPos, transform.position.z);
        //transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
    }

    //true == x and false == y
    private void transformPos()
    {
        //modify x position
        if(meteorXPos> GameController.instance.cameraPos.position.x + 5)
        {
            xPos = GameController.instance.cameraPos.position.x + 5;
        }
        else if (meteorXPos < GameController.instance.cameraPos.position.x - 5)
        {
            xPos = GameController.instance.cameraPos.position.x - 5;
        }
        else
        {
            xPos = meteorXPos;
        }
        
        //modify y position
        if(meteorYPos > GameController.instance.cameraPos.position.y + 2.5)
        {
            yPos = GameController.instance.cameraPos.position.y + 2.5f;
        }
        else
        {
            yPos = meteorYPos;
        }

        //modify the scale

    }*/

    //new version

    private Transform playerPos;
    private Animator anim;

    private void Start()
    {
        playerPos = GameController.instance.playerPos;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 1, playerPos.position.z);

        anim.SetBool("isOn", GameController.instance.meteorClose);
    }
}