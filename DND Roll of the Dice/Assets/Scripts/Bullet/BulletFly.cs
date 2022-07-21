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
        rb.velocity = transform.right * flySpeed;
    }

    private void Update()
    {
        if (bulletTrail != null)
        {
            Instantiate(bulletTrail, transform.position, Quaternion.identity);
        }
    }

    public void KillBullet()
    {
        Instantiate(bulletEndEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
