using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D6Health :EnemyHealth
{

    public override void OnTriggerEnter2D(Collider2D other)
    {
        D6Movement em = GetComponent<D6Movement>();

        if (em)
        {
            if (GetComponent<DiceValue>().value != 6)
            {
                if (other.CompareTag("PBullet"))
                {
                    TakeDamage();
                    other.GetComponent<BulletFly>().KillBullet();
                }

                if (other.CompareTag("Player"))
                {
                    Die();
                }
            }
            else if (GetComponent<DiceValue>().value == 6)
            {
                am.Play("D6 Harden");
                other.GetComponent<BulletFly>().KillBullet();
            }
        }
    }
}
