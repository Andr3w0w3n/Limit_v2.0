using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public float deleteWaitTime = 2;
    private float timePassed = 0;
    private bool hasBeenTouched = false;

    private float existanceTime = 0;
    public float totalTimeOnGround = 15;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(existanceTime >= totalTimeOnGround)
        {
            Destroy(this.gameObject);
        }
        if (hasBeenTouched)
        {
            if(timePassed>= deleteWaitTime)
            {
                Object.Destroy(gameObject);
            }
            timePassed += Time.fixedDeltaTime;
        }
        existanceTime += Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScaredStudent" /*&& !hasBeenTouched*/)
        {
            hasBeenTouched = true;
        }
    }
}
