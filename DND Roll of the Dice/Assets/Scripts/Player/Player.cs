using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats;

    private Animator animator;
    private PlayerMovement movement;
    private PlayerHealth health;
    private PlayerWeapon weapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        weapon = GetComponentInChildren<PlayerWeapon>();

        weapon.dmgPerAbilityUse = playerStats.dmgPerAbilityUse;
    }
    private void Start()
    {
        animator.runtimeAnimatorController = playerStats.anim;
        weapon.bulletPrefab = playerStats.projectile;
        weapon.ability = playerStats.ability.GetComponent<IClassAbility>();
        weapon.abilitySlider.transform.Find("Mask").transform.Find("Fill").GetComponent<Image>().color = playerStats.abilityUIColor;
        weapon.abilitySlider.transform.Find("Icon").GetComponent<Image>().sprite = playerStats.abilityUIIcon;
    }
}
