using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image healthbar;
    [SerializeField] private GameObject healParticles;

    private HitFlash playerFlash;
    private HitFlash healthBarFlash;

    private SpriteRenderer renderer;
    private CameraShaker camShake;
    private AudioManager audio;

    private void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        camShake = FindObjectOfType<CameraShaker>();
        renderer = GetComponent<SpriteRenderer>();

        playerFlash = new HitFlash(this, renderer, 0.25f);
        healthBarFlash = new HitFlash(this, healthbar, 0.25f);
    }

    private void Update()
    {
        healthbar.sprite = sprites[playerHealth];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
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
                if (playerHealth != 5 || playerHealth! > 5)
                {
                    HealDamage(1);
                    Instantiate(healParticles, transform.position, Quaternion.identity);
                }
                else audio.Play("Health Max");

                Destroy(collision.gameObject);
            }
        }
    }

    void HealDamage(int heal)
    {
        audio.Play("Heal");
        playerHealth += heal;
        playerFlash.Flash();
        healthBarFlash.Flash();
    }

    void TakeDamage(int dmg)
    {
        playerHealth -= dmg;

        audio.Play("Player Hurt");
        camShake.Shake();
        playerFlash.Flash();
        healthBarFlash.Flash();
    }
}
