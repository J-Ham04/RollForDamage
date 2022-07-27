using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerWeapon : InputManager
{
    // FIELDS
    [Header("Bullet Stats")]
    public GameObject bulletPrefab;
    [Space]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;

    [Header("Weapon Stats")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBtwShot;
    private float cooldown;

    [Header("Ability")]
    public Slider abilitySlider;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer shootingArrow;

    private AudioManager am;
    private Animator anim;

    private bool isShooting;
    public bool controllerAutoShoot;

    //Ability Variables
    [HideInInspector]
    public float dmgPerAbilityUse;
    private float dmgDealtSinceLastAbilityUse;
    public IClassAbility ability;

    // METHODS
    private void Awake()
    {
        base.Awake();

        am = FindObjectOfType<AudioManager>();
        anim = GetComponentInParent<Animator>();

        controls = new PlayerControls();
    }

    private void Start()
    {
        abilitySlider.maxValue = dmgPerAbilityUse;

        dmgDealtSinceLastAbilityUse = 0;
    }

    void Update()
    {
        base.Update();
        abilitySlider.value = dmgDealtSinceLastAbilityUse;

        isShooting = controls.Gameplay.Shoot.IsPressed();
        cooldown -= Time.deltaTime;

        if (usingController)
        {
            controls.Gameplay.AimPositionJoystick.performed += ctx => PointAtJoystick(ctx.ReadValue<Vector2>());
            controls.Gameplay.AimPositionJoystick.canceled += ctx => PointAtJoystick(Vector2.zero);
        }
        else PointAtMouse();

        if(usingController && controllerAutoShoot)
        {
            isShooting = true;
        }

        if (isShooting)
        {
            Shoot();
        }

        if(dmgDealtSinceLastAbilityUse >= dmgPerAbilityUse && controls.Gameplay.UseAbility.IsPressed())
        {
            UseAbility();
        }
    }

    private void PointAtMouse()
    {
        Cursor.visible = true;
        shootingArrow.enabled = false;

        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void PointAtJoystick(Vector2 jPos)
    {
        Cursor.visible = false;

        if (jPos != Vector2.zero)
        {
            shootingArrow.enabled = true;
        }
        else shootingArrow.enabled = false;

        Vector2 lookDir = jPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Shoot()
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

    public void AutoShootToggle(bool toggle)
    {
        controllerAutoShoot = toggle;
    }

    public void DealtDamage(int dmg)
    {
        dmgDealtSinceLastAbilityUse += dmg;
    }

    private void UseAbility()
    {
        dmgDealtSinceLastAbilityUse = 0;
        anim.SetTrigger("ability");
        ability.Ability(GetComponentInParent<PlayerMovement>().transform);
    }
}
