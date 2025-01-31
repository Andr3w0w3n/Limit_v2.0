﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMarkerBehavior : MonoBehaviour
{
    private bool goDown = true;

    public float moveDownSpeed = 0.005f;
    public float keyJump = .75f;
    public float timeActive = 30;
    // low: -1.7  High: 5.45
    void FixedUpdate()
    {
        if(transform.position.x <= -1.7f-1.8)
        {
            goDown = false;
        }
        else
        {
            goDown = true;
        }

        if (goDown)
        {
            transform.position = new Vector2(transform.position.x - moveDownSpeed, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.x < 5.45f-1.8)
        {
            transform.position = new Vector2(transform.position.x + keyJump, transform.position.y);
        }
    }
}
