using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public float lifeValue = 100;
    public float stanimaValue = 25;
    public float timeValue = 50;
    public bool isAlive = true;
    public static GameController instance;
    public int collected = 0;
    public int quickTimeScore = 0;
    public float speed = 1;
    public float timeSlowSpeed = .50f;
    public bool timeSlowActive = false;
    public float meteorCollisionSpeed = 1;

    public Transform playerPos;

    //awake method
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!isAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            timeSlowActive = !timeSlowActive;
        }
        if (timeSlowActive)
        {
            timeValue -= .1f;
            speed = timeSlowSpeed;
            if(timeValue <= 0)
            {
                timeSlowActive = false;
            }
        }
        else
        {
            speed = 1;
            if (timeValue < 50)
            {
                timeValue += .01f;
            }
        }

        //check to see if player is alive
        if(lifeValue <= 0)
        {
            isAlive = false;
        }
    }
}
