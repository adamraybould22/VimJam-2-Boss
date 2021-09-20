using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    // THe Brain of the AI
    private FSM Brain;

    // Components
    private SpriteRenderer Sprite;
    private Rigidbody2D Rig;
    private Animator Animator;

    [Header("Movement")]
    [Tooltip("Movement Speed of the Player")]
    public float moveSpeed = 3.5f;
    //The Movement from the Input
    private Vector3 movement;

    void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Rig = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();        
    }

    void Start()
    {
        Brain = new FSM(LookForWeapon);
    }

    void Update()
    {
        Brain.Update();
    }

    // Physics Update
    void FixedUpdate()
    {
        transform.position += movement * moveSpeed * Time.deltaTime;
        //Rig.MovePosition(Rig.position + movement * moveSpeed * Time.fixedDeltaTime);   

        // Movement Animation
        bool running = movement != Vector3.zero;
        Animator.SetBool("Run", running); 

        //Flip Sprite (Only Flip if Running and Movement is Character is moving Horizontally)
        if(running && movement.x != 0)
            gameObject.transform.localScale = new Vector3(Mathf.Sign(movement.x), 1, 1);
    }

    private void LookForWeapon()
    {
        GameObject weapon = GetClosestWeapon();
        movement = (weapon.transform.position - transform.position).normalized;

        if(Vector3.Distance(transform.position, weapon.transform.position) < 1)
        {
            GetComponent<Handler>().PickUpItem();
            if(GetComponent<Handler>().item.pickedUp)
            {
                Brain.SetState(AttackEnemies);
            }
        }
    }

    private GameObject GetClosestWeapon()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        GameObject closestWeapon = null;
        float distance = 100000;
        foreach(GameObject weapon in weapons)
        {
            if(distance >= Vector3.Distance(transform.position, weapon.transform.position))
            {
                distance = Vector3.Distance(transform.position, weapon.transform.position);
                closestWeapon = weapon;
            }
        }

        return closestWeapon;
    }

    private void AttackEnemies()
    {
        GameObject enemy = GetClosestEnemy();
        if(!enemy)
        {
            enemy = GetOpponent();
        }

        movement = (enemy.transform.position - transform.position).normalized;

        if(Vector3.Distance(enemy.transform.position, transform.position) < GetComponent<Handler>().item.GetComponent<MeleeWeapon>().damageRange)
        {
            GetComponent<Handler>().UseItem();
        }    
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

        GameObject closestEnemy = null;
        float distance = 100000;
        foreach(GameObject enemy in enemies)
        {
            if(distance >= Vector3.Distance(transform.position, enemy.transform.position))
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void AttackPlayer()
    {
        GameObject player = GetOpponent();
        movement = (player.transform.position - transform.position).normalized;
    }

    private GameObject GetOpponent()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); 
        GameObject playerObj = null;

        foreach(GameObject player in players)
        {
            if(player == gameObject)
                continue;

            playerObj = player;
        }

        return playerObj;
    }
}
