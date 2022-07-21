using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D6Health :EnemyHealth
{

    public override void OnTriggerEnter2D(Collider2D other)
    {
        D6Movement em = GetComponent<D6Movement>();

        if (em == null)
        {
            return;            
        }

        if (GetComponent<DiceValue>().value != 6)
        {
            CheckCollisions(other);
        }
        else if (GetComponent<DiceValue>().value == 6)
        {
            am.Play("D6 Harden");
            other.GetComponent<BulletFly>().KillBullet();
        }
    }
}
