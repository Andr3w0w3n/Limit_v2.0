using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private bool isAttackingRight = false;
    private bool isAttackingLeft = false;
    private bool isAttackingGround = false;
    private bool isAttackingMid = false;

    public Viewfinder rightView;
    public Viewfinder leftView;
    public Viewfinder groundView;
    public Viewfinder midView;

    public float movementSpeed = 5;
    public float jumpForce = 10;

    private Vector2 movementVector;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
