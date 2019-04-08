using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private Transform spawnerTr;

    public GameObject carObj;
    private GameObject spawnedCar;
    public Transform playerPos;

    public float minSpawnWaitTime = 5;
    public float maxSpawnWaitTime = 10;
    private float spawnWaitTime;
    //private bool needsNewSpawn = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnerTr = GetComponent<Transform>();
        spawnWaitTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        spawnerTr.position = new Vector2(playerPos.position.x + 30, -3.75f);
        if (timer >= spawnWaitTime)
        {
            spawnWaitTime = Random.Range(spawnTimeFunction(minSpawnWaitTime), spawnTimeFunction(maxSpawnWaitTime));
            timer = 0;
            spawnedCar = Instantiate(carObj, transform.position, Quaternion.identity);
        }
    }

    private float spawnTimeFunction(float origionalVal)
    {
        if (playerPos.position.x < 30)
        {
            return origionalVal / .5f;
        }
        else if (playerPos.position.x < 70)
        {
            return origionalVal;
        }
        else if (playerPos.position.x < 100)
        {
            return origionalVal / 1.5f;
        }
        else if (playerPos.position.x < 150)
        {
            return origionalVal / 2;
        }
        else
        {
            return origionalVal / 2.5f;
        }

    }
}