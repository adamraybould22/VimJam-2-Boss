using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [Tooltip("The Max Health of the Entity")]
    public int maxHealth;

    [Tooltip("The Current Health of the Entity")]
    public int health;

    [Tooltip("The Sprite to Change to when Damaged")]
    public Sprite damageSprite;

    private SpriteRenderer SpriteRenderer;
    private Rigidbody2D Rig;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rig = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(int damage, int knockback, Vector3 damagePos)
    {
        health -= damage; //Deal Damage
        Rig.AddForce((transform.position - damagePos).normalized * knockback, ForceMode2D.Impulse); //Deal KnockBack
        StartCoroutine(DamageFlash());

        if(health <= 0)
            Kill();
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private IEnumerator DamageFlash()
    {
        Shader GUIShader = Shader.Find("GUI/Text Shader");
        Shader defaultShader = Shader.Find("Sprites/Default");

        // Change Sprite to White
        SpriteRenderer.material.shader = GUIShader;
        SpriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        // Change Sprite Back
        SpriteRenderer.material.shader = defaultShader;
        SpriteRenderer.color = Color.white;
    }
}
