using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // FIELDS
    [Header("Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;

    [Header("Visuals")]
    [SerializeField] private GameObject healParticles;
    [SerializeField] private PlayerHealthBar healthBarUI;

    private bool _isInvulnerable;

    private HitFlash playerFlash;

    private SpriteRenderer renderer;
    private CameraShaker camShake;
    private AudioManager audio;

    // PROPERTIES
    public int health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value > maxHealth)
            {
                _health = maxHealth;
            }else
            {
                _health = value;
            }
        }
    }
    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    public bool isInvulnerable
    {
        get
        {
            return _isInvulnerable;
        }

        set
        {
            _isInvulnerable = value;
        }
    }

    // METHODS
    private void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        camShake = FindObjectOfType<CameraShaker>();
        renderer = GetComponent<SpriteRenderer>();

        playerFlash = new HitFlash(this, renderer, 0.25f);

        health = maxHealth;

        healthBarUI.RefreshHealthBarSize();
        healthBarUI.RefreshHealthBarDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            collision.GetComponent<BulletFly>().KillBullet();
        }

        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(2);
        }

        if (collision.CompareTag("Shockwave"))
        {
            TakeDamage(1);
        }

        if (collision.CompareTag("HP Pickup"))
        {
            Destroy(collision.gameObject);
            HealDamage(1, true);
            Instantiate(healParticles, transform.position, Quaternion.identity);

            if (health >= maxHealth)
            {
                audio.Play("Health Max");
            }
        }

        if (collision.CompareTag("Max HP Up"))
        {
            maxHealth++;

            healthBarUI.RefreshHealthBarSize();

            Destroy(collision.gameObject);
        }
    }

    public void HealDamage(int heal, bool playAudio)
    {
        health += heal;

        if (playAudio)
        {
            audio.Play("Heal");
        }

        healthBarUI.RefreshHealthBarDamage();
    }

    void TakeDamage(int dmg)
    {
        if (isInvulnerable == true)
        {
            return;
        }

        health -= dmg;

        audio.Play("Player Hurt");
        camShake.Shake();

        playerFlash.Flash();

        healthBarUI.RefreshHealthBarDamage();
    }
}
