using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Health : EnemyHealth
{
    public override void Die()
    {
        if (deathAnimation)
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
        }

        am.Play("D20 Explosion");

        sm.score += GetComponent<DiceValue>().value * 10;
        Destroy(gameObject);
    }
}
