using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Movement : EnemyMovement
{
    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 playerPos;
    private Animator anim;

    public float moveSpeedCap;
    public float moveSpeed;

    private float curSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if (playerPos.y >= transform.position.y)
        {
            anim.SetBool("rollUp", true);
        }else
        {
            anim.SetBool("rollUp", false);
        }

    }

    private void FixedUpdate()
    {
        if(stunned == false)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 lookDir = playerPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.velocity += lookDir * moveSpeed;

        curSpeed = rb.velocity.magnitude;

        if (curSpeed > moveSpeedCap)
        {
            float reduction = moveSpeedCap / curSpeed;
            rb.velocity *= reduction;
        }
    }
}
