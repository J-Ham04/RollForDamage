using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D8Movement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed;
    [SerializeField] private float moveTime;
    [SerializeField] private GameObject shockwave;

    private float cooldown;
    public float cooldownSet;

    bool isMoving;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.SetTrigger("jump");
        anim.SetBool("moving", false);
        cooldown = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            if (isMoving == false)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        isMoving = true;
        StartCoroutine(CO_Move());
    }

    private IEnumerator CO_Move()
    {
        anim.SetTrigger("jump");

        yield return new WaitForSeconds(0.065f);
        anim.SetBool("moving", true);

        Vector2 playerPos = player.transform.position;

        Vector2 angle = new Vector2(playerPos[0] - transform.position.x, playerPos[1] - transform.position.y);
        Vector2 normAngle = angle.normalized;

        rb.velocity = normAngle * moveSpeed;

        yield return new WaitForSeconds(moveTime);

        cooldown = cooldownSet;

        isMoving = false;
        anim.SetBool("moving", false);

    }

    public void Shockwave()
    {
        Instantiate(shockwave, transform.position, Quaternion.identity);
    }
}
