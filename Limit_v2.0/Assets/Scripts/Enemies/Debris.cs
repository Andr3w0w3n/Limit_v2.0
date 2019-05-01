using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movementVec;

    public float movementSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        movementVec = new Vector2(0, movementSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = movementVec * GameController.instance.speed;
        if (transform.position.y < -7)
        {
            Object.Destroy(gameObject);
        }
    }
}
