using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject fireMeteorObj;
    public GameObject meteorObj;
    GameObject[] meteorArr;
    public int numOfMeteors = 5;
    public float minMeteorSize = 0.5f;
    public float maxMeteorSize = 3;
    private float timePassed = 0;
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;
    private float spawnTime;
    public float ySpawnPosition;
    private int meteorNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        //intial construction of array
        meteorArr = new GameObject[numOfMeteors];
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime; 
    }

    void FixedUpdate()
    {
        Vector3 spawnPoint3D = new Vector3(Random.Range(0, 120), ySpawnPosition, transform.position.z);
        //if array consists of a null object, fill it
        for (int i = 0; i<meteorArr.Length; i++)
        {
            if(meteorArr[i] == null)
            {
                if (GetRandomBool())
                {
                    meteorArr[i] = (GameObject)Instantiate(fireMeteorObj, spawnPoint3D, Quaternion.identity);
                }
                else
                {
                    meteorArr[i] = (GameObject)Instantiate(meteorObj, spawnPoint3D, Quaternion.identity);
                }
                meteorArr[i].transform.localScale = meteorArr[i].transform.localScale * Random.Range(minMeteorSize, maxMeteorSize);
                meteorArr[i].gameObject.layer = 15;
            }
        }
        if(timePassed >= spawnTime)
        {
            meteorArr[meteorNum].transform.position = spawnPoint3D;
            timePassed = 0;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            if(meteorNum >= meteorArr.Length-1)
            {
                meteorNum = 0;
            }
            else
            {
                meteorNum++;
            }
        }
    }

    private bool GetRandomBool()
    {
        return ((int)Random.Range(0, 100)) % 2 == 0;
    }
}
