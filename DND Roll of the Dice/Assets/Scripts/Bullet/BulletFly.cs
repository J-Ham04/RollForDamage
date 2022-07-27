using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    // FIELDS
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private GameObject bulletEndEffect;

    Rigidbody2D rb;

    // PROPERTIES
    [HideInInspector]
    public float flySpeed { get; set; }

    // METHODS
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity += new Vector2(transform.right.x, transform.right.y) * flySpeed;
    }

    private void Update()
    {
        SpawnBulletTrail();
    }

    private void SpawnBulletTrail()
    {
        if (bulletTrail == null)
        {
            return;
        }

        Instantiate(bulletTrail, transform.position, Quaternion.identity);
    }

    public void KillBullet()
    {
        Instantiate(bulletEndEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
