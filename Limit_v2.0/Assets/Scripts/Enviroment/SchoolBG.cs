using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SchoolBG : MonoBehaviour
{
    GameObject[] enemySpawnerArr;
    private bool[] enemyCheckArr;
    private BoxCollider2D backgroundCollider;
    private float backgroundHorizontalLength;
    public Transform playerTransform;
    public int numOfMinEnemies = 0;
    public int numOfMaxEnemies = 5;
    private int numOfEnemies;
    private int spawnXPosition;
    public GameObject ScaredStudent_A01;
    private Vector3 spawnPoint3;
    private Vector2 spawnPoint2;
    private float timeSinceLast;
    public float maxSpawnRate;
    private float spawnRate = 0;
    private int currentEnemy;
    private Animator animationBG;
    private float timer;
    public float spawnTimeIncrement;
    // Start is called before the first frame update
    void Start()
    {
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundHorizontalLength = backgroundCollider.size.x;
        animationBG = GetComponent<Animator>();
        currentEnemy = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update variables
        timeSinceLast += Time.deltaTime;
        spawnPoint3 = new Vector3(transform.position.x + (3.7f+.6f), 0.075f, -1.01f);
        spawnPoint2 = new Vector2(transform.position.x + (3.7f + .6f), 0.075f);


        //create and fill arrays if they are empty
        if(enemySpawnerArr == null)
        {
            enemySpawnerArr = new GameObject[(int)Random.Range(numOfMinEnemies, numOfMaxEnemies)];
            numOfEnemies = enemySpawnerArr.Length;
            enemyCheckArr = new bool[numOfEnemies];
            for (int i = 0; i < numOfEnemies; i++)
            {
                enemySpawnerArr[i] = (GameObject)Instantiate(ScaredStudent_A01, spawnPoint3, Quaternion.identity);
                enemyCheckArr[i] = true;
            }
        }

        //update spawn rate slowly, making the spawn rate go up slowly
        if(spawnRate >= maxSpawnRate - 1 && (timer >= spawnTimeIncrement))
        {
            spawnRate = maxSpawnRate;
        }
        else if(timer >= spawnTimeIncrement)
        {
            spawnRate++;
            timer = 0f;
        }

        //check to see if all enemies still exist in the game, if not, fill the array
        for(int i = 0; i<enemySpawnerArr.Length; i++)
        {
            if(enemySpawnerArr[i] == null)
            {
                enemySpawnerArr[i] = (GameObject)Instantiate(ScaredStudent_A01, spawnPoint3, Quaternion.identity);
                enemyCheckArr[i] = true;
            }
        }

        //autobots roll out!!!
        if (timeSinceLast >= spawnRate)
        {
            if(enemySpawnerArr.Length != 0)
            {
                if (enemyCheckArr[currentEnemy] == false)
                {
                    currentEnemy++;
                }
                else
                {
                    enemySpawnerArr[currentEnemy].transform.position = spawnPoint3;
                    enemyCheckArr[currentEnemy] = false;
                    currentEnemy++;
                    animationBG.SetTrigger("Open");
                }
                if (currentEnemy >= numOfEnemies - 1)
                {
                    currentEnemy = 0;
                }
            }  
        }

        //changing BG location
        if (playerTransform.position.x > transform.position.x + (backgroundHorizontalLength / 2 + backgroundHorizontalLength * 2))
        {
            repositionBG(true);
        }
        else if (playerTransform.position.x < transform.position.x - (backgroundHorizontalLength / 2 + backgroundHorizontalLength * 2))
        {
            repositionBG(false);
        }
    }

    void Update()
    {
        //to prevent updating when no longer needed (is this useless?)
        if(timer !< 0)
        {
            timer += Time.deltaTime;
        }
    }
    //method for repositioning the bg
    private void repositionBG(bool frontOrBack)
    {
        if (frontOrBack)
        {
            Vector3 positiveGroundMove = new Vector3(backgroundHorizontalLength * 5f, 0, 0);
            transform.position = (Vector3)transform.position + positiveGroundMove;
        }
        else
        {
            Vector3 positiveGroundMove = new Vector3(backgroundHorizontalLength * -5f, 0, 0);
            transform.position = transform.position + positiveGroundMove;
        }
    }
}
