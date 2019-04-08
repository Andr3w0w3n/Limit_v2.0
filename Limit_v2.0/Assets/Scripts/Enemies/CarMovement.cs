using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movementVec;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementVec = new Vector2(-speed, 0);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movementVec * GameController.instance.speed;
        if(transform.position.x < GameController.instance.playerPos.position.x - 20)
        {
            Object.Destroy(gameObject);
        }
    }
}
