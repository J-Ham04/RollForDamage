using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // FIELDS
    [SerializeField] protected GameObject deathAnimation;

    protected ScoreManager sm;
    protected AudioManager am;

    private SpriteRenderer spriteRend;

    private HitFlash hit;

    [SerializeField] private int _health;

    // PROPERTIES
    public int health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;

            if (value <= 0)
            {
                Die();
            }
        }
    }

    // METHODS
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        am = FindObjectOfType<AudioManager>();

        hit = new HitFlash(this, spriteRend);
        health = _health;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }

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

    protected void TakeDamage()
    {
        am.Play("Enemy Hurt");
        health -= 1;
        hit.Flash();
    }


    public virtual void Die()
    {
        if (deathAnimation)
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
        }

        am.Play("Enemy Death");

        sm.score += GetComponent<DiceValue>().value * 10;
        Destroy(gameObject);
    }
}
