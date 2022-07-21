using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    PlayerControls controls;

    // FIELDS
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float timeBtwShot;
    private float cooldown;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float bulletLifetime;

    [SerializeField] private SpriteRenderer shootingArrow;

    private AudioManager am;

    private bool isShooting;
    private bool usingController;

    // METHODS
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();

        controls = new PlayerControls();
    }

    void Update()
    {
        isShooting = controls.Gameplay.Shoot.IsPressed();
        usingController = controls.Gameplay.AimPositionJoystick.IsPressed();

        if (usingController)
        {
            controls.Gameplay.AimPositionJoystick.performed += ctx => PointAtJoystick(ctx.ReadValue<Vector2>());
        }
        else PointAtMouse();

        if (isShooting)
        {
            Shoot();
        }
        cooldown -= Time.deltaTime;
    }

    private void PointAtMouse()
    {
        Cursor.visible = true;
        shootingArrow.enabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void PointAtJoystick(Vector2 jPos)
    {
        Cursor.visible = false;
        shootingArrow.enabled = true;

        Vector2 lookDir = jPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (cooldown > 0)
        {
            return;
        }

        am.Play("Player Shoot");
        GameObject obj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletFly bullet = obj.GetComponent<BulletFly>();

        bullet.flySpeed = bulletSpeed;
        bullet.Invoke("KillBullet", bulletLifetime);

        cooldown = timeBtwShot;
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
