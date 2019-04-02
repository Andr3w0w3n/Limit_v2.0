using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockerDetection : MonoBehaviour
{
    //globals
    private PlatformEffector2D pfe;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        pfe = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                pfe.rotationalOffset = 180;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pfe.rotationalOffset = 0;
        }
    }
}
