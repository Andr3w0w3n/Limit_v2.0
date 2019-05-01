using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawn : MonoBehaviour
{
    public Transform playerTransform;
    private BoxCollider2D backgroundCollider;
    private float backgroundHorizontalLength;
    // Start is called before the first frame update
    void Start()
    {
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundHorizontalLength = backgroundCollider.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //changing BG location
        if (playerTransform.position.x > transform.position.x + (backgroundHorizontalLength / 2 + backgroundHorizontalLength * 2))
        {
            repositionBG(true);
        }
        else if (playerTransform.position.x < transform.position.x - (backgroundHorizontalLength / 2 + backgroundHorizontalLength * 2))
        {
            repositionBG(false);
        }
    }
    //method for repositioning the bg
    private void repositionBG(bool frontOrBack)
    {
        if (frontOrBack)
        {
            Vector3 positiveGroundMove = new Vector3(backgroundHorizontalLength * 5f, 0, 0);
            transform.position = (Vector3)transform.position + positiveGroundMove;
        }
        else
        {
            Vector3 positiveGroundMove = new Vector3(backgroundHorizontalLength * -5f, 0, 0);
            transform.position = transform.position + positiveGroundMove;
        }
    }
}

