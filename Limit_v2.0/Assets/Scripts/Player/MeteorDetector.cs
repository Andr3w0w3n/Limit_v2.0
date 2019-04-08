using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDetector : MonoBehaviour
{
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if(playerTransform == null)
        {
            Debug.Log("you do not have a transform attached");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(playerTransform.position.x, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Meteor")
        {
            GameController.instance.meteorClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Meteor")
        {
            GameController.instance.meteorClose = false;
        }
    }
}
