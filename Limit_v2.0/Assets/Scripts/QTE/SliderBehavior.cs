using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBehavior : MonoBehaviour
{
    public float startTime = 10;
    public float endTime = 70;
    private float timer = 0;
    public bool isMarker = false;

    private SpriteRenderer sprtR;
    private bool isAwake = false;
    // Start is called before the first frame update
    void Start()
    {
        sprtR = GetComponent<SpriteRenderer>();
        sprtR.color = new Color(sprtR.color.r, sprtR.color.g, sprtR.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!isAwake && timer >= startTime)
        {
            isAwake = true;
            sprtR.color = new Color(sprtR.color.r, sprtR.color.g, sprtR.color.b, 100);
        }else if(timer >= endTime)
        {
            if (isMarker)
            {
                GameController.instance.quickTimeScore += sliderScore(transform.position.x);
            }
            Object.Destroy(gameObject);
        }
    }
    private int sliderScore(float pos)
    {
        if(pos > 2.5)
        {
            return 30;
        }
        else if(pos > -1.5)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
}
