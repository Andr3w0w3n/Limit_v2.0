using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDebrisBehavior : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(transform.rotation.z < 0)
        {
            movement = new Vector2(-5, 3);
        }
        else
        {
            movement = new Vector2(5, 3);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y >= 9 || (transform.position.x <= 12 || transform.position.x >= 12))
        {
            Object.Destroy(gameObject);
        }
        else
        {
            rb2d.velocity = movement*GameController.instance.speed;
        }
    }
}
