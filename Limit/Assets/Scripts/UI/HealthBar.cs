using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float lifeFillVal;
    public Image bar; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.lifeValue/100 != lifeFillVal)
        {
            lifeFillVal = GameController.instance.lifeValue/100;

            //deduct the bar accordingly
            bar.fillAmount = lifeFillVal;
        }
        
    }
}
