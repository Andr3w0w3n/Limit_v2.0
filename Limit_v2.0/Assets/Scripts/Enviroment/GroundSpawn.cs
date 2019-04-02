using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    //globals
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.x > transform.position.x+(groundHorizontalLength/2+groundHorizontalLength*2))
        {
            repositionGround(true);
        }else if(playerTransform.position.x < transform.position.x- (groundHorizontalLength / 2 + groundHorizontalLength*2))
        {
            repositionGround(false);
        }
    }

    private void repositionGround(bool frontOrBack)
    {
        if (frontOrBack)
        {
            Vector3 positiveGroundMove = new Vector3(groundHorizontalLength * 5f, 0, 0);
            transform.position  = transform.position + positiveGroundMove;
        }
        else
        {
            Vector3 positiveGroundMove = new Vector3(groundHorizontalLength * -5f, 0, 0);
            transform.position = transform.position + positiveGroundMove;
        }
    }
}
