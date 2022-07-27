using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D6Movement : EnemyMovement
{
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb;
    private D6Shoot es;
    public DiceValue diceValue;

    public float moveSpeed;

    private float cooldown;
    public float cooldownSet;

    bool isMoving;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        es = GetComponent<D6Shoot>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SetDiceValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }else if (isMoving == false && stunned == false)
        {
                Move();
        }
        if(stunned == true)
        {
            StopCoroutine(CO_Move());
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

        yield return new WaitForSeconds(0.35f);

        SetDiceValue();

        Vector2 playerPos = player.transform.position;

        Vector2 angle = new Vector2(playerPos[0] - transform.position.x, playerPos[1] - transform.position.y);
        Vector2 normAngle = angle.normalized;

        rb.velocity = normAngle * moveSpeed;

        cooldown = cooldownSet;
        isMoving = false;
    }

    void SetDiceValue()
    {
        diceValue.value = Random.Range(1, 7);
        anim.SetInteger("face", diceValue.value);
        es.NewDiceValue(diceValue.value + 1);
    }

    public void Stun()
    {

    }
}
