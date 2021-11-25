using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public GameObject debris;
    public GameObject smallDebris;
    public BossBehavior bossScript;

    public float minRandomAngle = -45;
    public float maxRandomAngle = 45;

    public float minSmall = 3;
    public float maxSmall = 7;

    private float timer = 0;
    private bool activeTimer = false;

    private GameObject spawnedDebris;
    private GameObject spawnedSmallDebris;

    // Update is called once per frame
    void Update()
    {
        if (activeTimer)
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (bossScript.isAttacking && (bossScript.playerAtLeft || bossScript.playerAtRight))
        {
            activeTimer = true;
            if(timer >= 126.0f / 60.0f)
            {
                activeTimer = false;
                if (bossScript.playerAtLeft)
                {
                    spawnedDebris = Instantiate(debris, new Vector2(-6, 10), Quaternion.Euler(0, 0, Random.Range(minRandomAngle, maxRandomAngle)));
                }
                else
                {
                    spawnedDebris = Instantiate(debris, new Vector2(6, 10), Quaternion.Euler(0,0,Random.Range(minRandomAngle, maxRandomAngle)));
                }
                timer = 0;
            }
        }
        if(spawnedDebris != null)
        {
            if (spawnedDebris.transform.position.y < -7)
            {
                Destroy(spawnedDebris);
            }
        } 
    }
}
