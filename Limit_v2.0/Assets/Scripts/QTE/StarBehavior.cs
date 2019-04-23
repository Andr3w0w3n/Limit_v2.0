using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehavior : MonoBehaviour
{
    private Animator anim;

    public int achieveScore = 10;

    // Start is called before the first frame update
    void Start()
    {
        if(achieveScore == 10)
        {
            Debug.Log("The achieve score has the default value of 10, make sure this is what you want");
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.quickTimeScore >= achieveScore)
        {
            anim.SetBool("isAchieved", true);
        }
    }
}
