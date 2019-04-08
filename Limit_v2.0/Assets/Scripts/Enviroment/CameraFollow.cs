using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //globals
    public Transform playerTransform;
    private Vector3 offset;

    public float yLowPosition = 1.6f;
    public float yTopPosition = 3.4f;
    public float xLeftPosition = -1.85f;
    public float xRightPosition = 128.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        if(transform.position.y < yLowPosition)
        {
            transform.position = new Vector3(transform.position.x, yLowPosition, transform.position.z);
        }
        if(transform.position.y > yTopPosition)
        {
            transform.position = new Vector3(transform.position.x, yTopPosition, transform.position.z);
        }
        if(transform.position.x < xLeftPosition)
        {
            transform.position = new Vector3(xLeftPosition, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRightPosition)
        {
            transform.position = new Vector3(xRightPosition, transform.position.y, transform.position.z);
        }
    }
}
