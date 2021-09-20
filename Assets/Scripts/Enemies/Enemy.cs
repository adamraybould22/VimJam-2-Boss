using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Components
    private SpriteRenderer Sprite;
    private Rigidbody2D Rig;
    private Animator Animator;

    [Tooltip("The Target of the Enemy")]
    public GameObject Target;

    [Tooltip("States if the Target can Change")]
    public bool canChangeTarget;

    [Header("Movement")]
    [Tooltip("Movement Speed of the Enemy")]
    public float moveSpeed = 3.5f;
    //The Movement from the Input
    private Vector3 movement;

    [Header("Points")]
    [Tooltip("The Number of Points the Enemy gives")]
    public int points;

    void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Rig = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();        
    }

    void Start()
    {
        Target = GetClosestPlayer();

        // Randomly Scale Enemy
        //float randomScale = Random.Range(0.8f, 1.1f);
        //gameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    // Update is called once per frame
    void Update()
    {
        if(canChangeTarget)
            Target = GetClosestPlayer();

        movement = (Target.transform.position - transform.position).normalized;

        if(Vector3.Distance(transform.position, Target.transform.position) < 0.5)
        {
            GetComponent<Handler>().UseItem();
        }
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
            gameObject.transform.localScale = new Vector3(Mathf.Sign(movement.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    private GameObject GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject closestPlayer = null;
        float distance = 10000;
        foreach(GameObject player in players)
        {
            if(distance >= Vector3.Distance(transform.position, player.transform.position))
            {
                distance = Vector3.Distance(transform.position, player.transform.position);
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}
