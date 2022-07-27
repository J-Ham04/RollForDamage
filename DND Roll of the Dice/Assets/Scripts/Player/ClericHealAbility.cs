using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericHealAbility : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth ph = collision.GetComponent<PlayerHealth>();
        EnemyHealth eh = collision.GetComponent<EnemyHealth>();

        if (ph)
        {
            ph.HealDamage(ph.maxHealth - ph.health, false);
        }

        if (eh)
        {
            eh.HealDamage(eh._maxHealth - eh.health);
        }
    }
}
