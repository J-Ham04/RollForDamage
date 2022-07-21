using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D6Shoot : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] private float bulletSpeed = 8;
    [SerializeField] private float bulletLifetime = 3;
    public GameObject bullet;

    [Space]
    public float rotateSpeed;
    public float cooldownSet;

    private float cooldown;
    [SerializeField] private List<Transform> firePoints;
    private List<Transform> activeFirePoints = new List<Transform>();
    private Rigidbody2D rb;

    private AudioManager am;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firePoints = new List<Transform>(GetComponentsInChildren<Transform>());
        firePoints.Remove(transform);
        cooldown = cooldownSet;
        am = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        for (int i = 0; i < firePoints.Count; i++)
        {
            if (firePoints[i].CompareTag("Light"))
            {
                firePoints.Remove(firePoints[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;

        } else if(rb.velocity.magnitude <= 0.1f)
        {
            Shoot();
        }

        foreach(Transform f in firePoints)
        {
            f.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }

    }
    private void Shoot()
    {
        am.Play("Enemy Shoot");
        foreach (Transform f in activeFirePoints)
        {
            GameObject obj = Instantiate(bullet, f.position, f.rotation);
            BulletFly bulletComp = obj.GetComponent<BulletFly>();

            bulletComp.flySpeed = bulletSpeed;
            bulletComp.Invoke("KillBullet", bulletLifetime);
        }

        cooldown = cooldownSet;

    }

    public void NewDiceValue(int newValue)
    {
        if(activeFirePoints.Count > 0)
        {
        activeFirePoints.Clear();
        }

        for (int i = 0; i < firePoints.Count; i++)
        {
            if (i < newValue - 1)
            {
                activeFirePoints.Add(firePoints[i]);
            }
        }
    }
}
