using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAbility : MonoBehaviour, IClassAbility
{
    public float dashSpeed;
    public float dashDuration;

    private Rigidbody2D playerRB;
    private PlayerHealth health;


    public void Ability(Transform playerTransform)
    {
        playerRB = playerTransform.gameObject.GetComponent<Rigidbody2D>();
        health = playerTransform.gameObject.GetComponent<PlayerHealth>();
        PlayerMovement pm = FindObjectOfType<PlayerMovement>();

        pm.Stun(dashDuration);
        playerRB.velocity = Vector2.zero;
        playerRB.velocity = pm.constantMoveInput * dashSpeed;
        health.StartCoroutine(InvulnerableTime());
    }

    IEnumerator InvulnerableTime()
    {
        health.isInvulnerable = true;
        yield return new WaitForSeconds(dashDuration);
        health.isInvulnerable = false;
    }

}
