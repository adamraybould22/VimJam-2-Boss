using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
    public enum WeaponType
    {
        MELEE,
        RANGED,
        MAGIC,
    }

    [Tooltip("The Type of Weapon")]
    public WeaponType Type;

    [Header("Damage")]
    [Tooltip("The Damage that the Weapon does")]
    public int Damage;

    [Tooltip("The Weapons Knockback")]
    public int Knockback;
}
