using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanimaBar : MonoBehaviour
{
    public Image bar;
    private float stanimaVal;
    // Start is called before the first frame update
    void Start()
    {
        stanimaVal = GameController.instance.stanimaValue;
        if(bar == null)
        {
            bar = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.stanimaValue != stanimaVal)
        {
            stanimaVal = GameController.instance.stanimaValue;
            bar.fillAmount = (stanimaVal * 4) / 100;
        }
    }
}
