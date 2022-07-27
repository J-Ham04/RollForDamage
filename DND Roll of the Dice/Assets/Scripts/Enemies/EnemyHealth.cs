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

    public int _maxHealth;
    public int _health;

    PlayerWeapon playerWeapon;

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
    private void Awake()
    {
        playerWeapon = FindObjectOfType<PlayerWeapon>();
    }

    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        am = FindObjectOfType<AudioManager>();

        hit = new HitFlash(this, spriteRend);
        _health = _maxHealth;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }

        CheckCollisions(other);
    }

    protected void CheckCollisions(Collider2D other)
    {
        if (other.CompareTag("PBullet"))
        {
            TakeDamage(5);
            other.GetComponent<BulletFly>().KillBullet();
        }

        if (other.CompareTag("Player"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int dmg)
    {
        playerWeapon.DealtDamage(dmg);
        am.Play("Enemy Hurt");
        health -= dmg;
        hit.Flash();

    }

    public void HealDamage(int heal)
    {
        health += heal;
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
