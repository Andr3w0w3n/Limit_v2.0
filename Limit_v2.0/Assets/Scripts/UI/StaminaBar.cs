using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image bar;
    private float staminaVal;
    // Start is called before the first frame update
    void Start()
    {
        staminaVal = GameController.instance.staminaValue;
        if(bar == null)
        {
            bar = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.staminaValue != staminaVal)
        {
            staminaVal = GameController.instance.staminaValue;
            bar.fillAmount = (staminaVal * 4) / 100;
        }
    }
}
