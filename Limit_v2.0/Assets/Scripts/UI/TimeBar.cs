using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Image bar;
    private float timeVal;
    // Start is called before the first frame update
    void Start()
    {
        timeVal = GameController.instance.timeValue;
        if (bar == null)
        {
            Debug.Log("You do not have anything assigned to the Image, defaulted to attached image");
            bar = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.timeValue != timeVal)
        {
            timeVal = GameController.instance.timeValue;
            bar.fillAmount = (timeVal) / 50;
        }
    }
}
