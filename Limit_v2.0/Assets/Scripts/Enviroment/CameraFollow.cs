using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //globals
    public Transform playerTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        if(transform.position.y < 1.6)
        {
            transform.position = new Vector3(transform.position.x, 1.6f, transform.position.z);
        }
        if(transform.position.y > 3.4)
        {
            transform.position = new Vector3(transform.position.x, 3.4f, transform.position.z);
        }
        if(transform.position.x < -1.85)
        {
            transform.position = new Vector3(-1.85f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 128.5)
        {
            transform.position = new Vector3(128.5f, transform.position.y, transform.position.z);
        }
    }
}
