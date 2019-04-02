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
            Debug.Log("There was a problem gettig the players transform, defualt transform applied.");
            playerTransform = GetComponent < Transform >();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
