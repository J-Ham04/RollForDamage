using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "Player Stats", fileName = "New Class")]
public class PlayerStats : ScriptableObject
{
    public RuntimeAnimatorController anim;
    public GameObject projectile;
    public GameObject ability;
    public int dmgPerAbilityUse;
    public Sprite abilityUIIcon;
    public Color abilityUIColor;
}
