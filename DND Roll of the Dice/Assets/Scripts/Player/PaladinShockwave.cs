using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinShockwave : MonoBehaviour
{
    [SerializeField] private int shockwaveDuration;
    [SerializeField] private float stunTime;
    [SerializeField] private float enemyStunTime;
    [SerializeField] private float blastSpeed;

    private AudioManager am;
    private CameraShaker camShake;

    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        camShake = FindObjectOfType<CameraShaker>();

        if (camShake)
        {
            camShake.Shake();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement em = collision.GetComponent<EnemyMovement>();
            EnemyHealth eh = collision.GetComponent<EnemyHealth>();

            if (em)
            {
                em.Stun(stunTime);
            }

            if (eh == null)
            {
                return;
            }
            else eh.TakeDamage(20);
        }


    }
}
