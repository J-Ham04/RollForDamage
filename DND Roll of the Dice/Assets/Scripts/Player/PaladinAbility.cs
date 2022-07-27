using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAbility : MonoBehaviour, IClassAbility
{

    public GameObject paladinShockwave;

    private PlayerHealth health;

    public void Ability(Transform playerTransform)
    {
        health = playerTransform.gameObject.GetComponent<PlayerHealth>();

        playerTransform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(SmashAbility(playerTransform));
    }

    IEnumerator SmashAbility(Transform playerTransform)
    {
        health.isInvulnerable = true;
        playerTransform.gameObject.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        playerTransform.gameObject.GetComponentInParent<PlayerMovement>().Stun(0.75f);
        yield return new WaitForSeconds(0.40f);
        Instantiate(paladinShockwave, playerTransform.position, Quaternion.identity);
        health.isInvulnerable = false;
    }
}
