using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericAbility : MonoBehaviour, IClassAbility
{

    private int healAmount = 3;

    public void Ability(Transform playerTransform)
    {
        PlayerHealth ph = playerTransform.GetComponent<PlayerHealth>();
        ph.HealDamage(healAmount, enabled);

        playerTransform.gameObject.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        playerTransform.gameObject.GetComponentInParent<PlayerMovement>().Stun(1f);
    }
}
