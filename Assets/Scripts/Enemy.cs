using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] float speed = 3f;
    [SerializeField] float rSpeed = 200f;
    [SerializeField] float rotAngle = 0f;
    public int startHealth = 10;
    Health enemyHealth;


    [Header("Physics")]
    //[SerializeField] layerMask wallMask;
    //[SerializeField] layerMask floorMask;
    //[SerializeField] layerMask element;

    [Header("Flavor")]
    //[SerializeField] string bugType = "n/a";

    //[Header("Tracked Data")]

    public Transform target;
    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        //enemyHealth = Health.setHealth(startHealth);
    }

    private void Update(){
        //get target
        if (!target){
             GetTarget();
        }else{
        //rotate towards target
        RotateTowardsTarget();
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

    private void GetTarget(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        //MeleeAttack.bugAttack();
    }

}
