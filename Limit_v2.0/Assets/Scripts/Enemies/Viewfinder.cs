using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewfinder : MonoBehaviour
{
    public bool playerInFrame = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInFrame = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInFrame = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInFrame = true;
        }
    }
}
