using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] public float speed = 3f;
    [SerializeField] public float rSpeed = 2f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public int startHealth = 10;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float interval = 0.3f;
    [SerializeField] MeleeAttack melee;


    [Header("Physics")]
    public string targetTag = "Player"; // Tag to search for

    [Header("Flavor")]
    [SerializeField] string bugType = "termite";


    Transform target;
    private Rigidbody2D rb;
    Health enemyHealth;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyHealth.Initialize(startHealth);
    }

    private void Update(){
        //get target
        if (!target){
             GetTarget();
        }
        //rotate towards target
        timer += Time.deltaTime;
        if (timer >= interval){
            RotateTowardsTarget();
            timer = 0f;
        }
    }

    private void FixedUpdate(){
        //move forwards
        rb.velocity = transform.up*speed;
    }

    private void RotateTowardsTarget(){
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3 (0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rSpeed);
    }

    void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        melee.bugAttack();
    }

}
