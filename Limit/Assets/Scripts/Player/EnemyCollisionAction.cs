using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionAction : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private int enemyTag;
    private Animator animationChar;
    private bool isHit = false;
    private float alphaLvl = 1;
    public int flashRepeats = 3;
    private SpriteRenderer spR;
    private int flashCounter = 0;
    private bool flashDown = true;
    private float timeOfAnim = 0;
    public float timeWantedOfAnim = 1;
    private bool triggerAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        spR = GetComponent<SpriteRenderer>();
        animationChar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHit)
        {
            flashCall();
            if (triggerAnim)
            {
                timeOfAnim += Time.fixedDeltaTime;
                animationChar.SetBool("isDamaged", true);
                if(timeOfAnim >= timeWantedOfAnim)
                {
                    triggerAnim = false;
                    animationChar.SetBool("isDamaged", false);
                    timeOfAnim = 0;
                }           
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ScaredStudent")
        {
            isHit = true;
            triggerAnim = true;
            GameController.instance.lifeValue -= 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            isHit = true;
            triggerAnim = true;
        }
    }

    private void damageTakenFlash(float lvl, bool down)
    {
        spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, alphaLvl);
        if (down)
        {
            alphaLvl = alphaLvl - 0.05f;
        }
        else
        {
            alphaLvl = alphaLvl + 0.05f;
        }      
    }

    private void flashCall()
    {
        //statements for damage "flashing" sprite
        if (isHit && alphaLvl > .5 && flashDown && flashCounter != flashRepeats)
        {
            damageTakenFlash(alphaLvl, flashDown);
        }
        else if (isHit && alphaLvl <= .5 && flashDown)
        {
            flashDown = false;
        }
        else if (isHit && alphaLvl < 1 && !flashDown && flashCounter != flashRepeats)
        {
            damageTakenFlash(alphaLvl, flashDown);
        }
        else if (isHit && alphaLvl >= 1 && !flashDown && flashCounter != flashRepeats)
        {
            flashDown = true;
            flashCounter++;
        }
        else if (flashCounter >= flashRepeats)
        {
            isHit = false;
            flashCounter = 0;
        }

        //ignore enemy collision while active
        if (isHit)
        {
            this.gameObject.layer = 13;
        }
        else
        {
            this.gameObject.layer = 8;
        }
    }
}