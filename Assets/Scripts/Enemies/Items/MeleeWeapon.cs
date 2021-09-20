using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Item
{
    public enum DamageTarget
    {
        Enemy,
        Player,
    }

    [Tooltip("The Target to do Damage Too")]
    public DamageTarget Target;

    [Tooltip("Data about the Weapon")]
    public Weapon Weapon;

    [Header("Combo Properties")]
    [Tooltip("The Number of Attacks in a Combo")]
    public int comboMax;

    [Tooltip("The Current Combo Index")]
    public int combo;

    [Tooltip("Specifies how long Before the Timer Resets")]
    public float comboTimer;

    [Header("Damage Properties")]
    [Tooltip("The Radius around the Damage Point")]
    public float damageRange;

    [Tooltip("The Point where a Weapon deals Damage")]
    public Transform damagePoint;

    private Animator Animator;

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        combo = 1;
    }

    public override void UseItem()
    {
        base.UseItem();

        DamageViaMelee();
        RunCombo();       
    }

    // Plays an Animation based on the Combo
    private void RunCombo()
    {
        if(comboMax <= 0 || !Animator)
            return;

        if(combo > comboMax)
            combo = 1;

        Animator.SetTrigger("Swing-" + combo);
        combo++;

        StopAllCoroutines();
        StartCoroutine(ResetCombo());
    }

    private IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(comboTimer);
        combo = 1;        
    }

    private void DamageViaMelee()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(damagePoint.position, damageRange, 1 << LayerMask.NameToLayer(Target.ToString()));
        foreach(Collider2D enemy in hitColliders)
        {
            // Ignore Self
            if(enemy == gameObject)
                continue;

            EntityHealth health = enemy.GetComponent<EntityHealth>();

            // Do more Damage on the Last Combo Attack
            if(combo == comboMax)       
            {
                health.Damage(Weapon.Damage * 2, Weapon.Knockback, Handler.transform.position);  
            }
            else
                health.Damage(Weapon.Damage, Weapon.Knockback, Handler.transform.position);

            if(health.IsDead())
            {
                Handler.GetComponent<PlayerPointer>().AddPoint(enemy.GetComponent<Enemy>().points);
            }
        }
    }
}
