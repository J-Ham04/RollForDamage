using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Crush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth eh = collision.GetComponent<EnemyHealth>();

        if (eh)
        {
            eh.Die();
        }else if (collision.CompareTag("Bullet"))
        {
            collision.GetComponent<BulletFly>().KillBullet();
        }
    }
}
