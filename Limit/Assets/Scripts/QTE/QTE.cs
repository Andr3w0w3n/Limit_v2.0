using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spR;
    public float timeTillTrigger = 15;
    public float timeActionLasts = 15;
    public string triggerName;
    private float timePassed = 0;
    private bool trigger = false;
    private bool done = false;
    public string keyLabel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spR = GetComponent<SpriteRenderer>();
        spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, 0);
        if(triggerName == null)
        {
            Debug.LogError("You do not have a name for the animation trigger");
        }
        if(keyLabel == null)
        {
            Debug.LogError("You do not have a key written, it must be lowercase");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeTillTrigger && !done)
        {
            trigger = true;
            spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, 100);
            timePassed = 0;          
        }        
    }

    private void FixedUpdate()
    {
        if (trigger)
        {
            if (Input.GetKeyDown(keyLabel))
            {
                GameController.instance.quickTimeScore++;
            }
            if (Input.GetKey(keyLabel))
            {
                anim.SetBool(triggerName, true);
            }
            else
            {
                anim.SetBool(triggerName, false);
            }
            if(timePassed >= timeActionLasts)
            {
                spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, 0);
                done = true;
                trigger = false;
            }
        }
    }
}
