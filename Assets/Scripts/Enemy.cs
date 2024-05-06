using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] public float speed = 3f;
    [SerializeField] public float rSpeed = 200f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public int startHealth = 10;
    Health enemyHealth;


    [Header("Physics")]
    //[SerializeField] layerMask wallMask;
    //[SerializeField] layerMask floorMask;
    //[SerializeField] layerMask element;
    public float detectionRange = 10f;
    public string targetTag = "entity"; // Tag to search for

    [Header("Flavor")]
    [SerializeField] string bugType = "termite";

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

    void GetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange); // Search area around the enemy

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTarget = collider.gameObject;
                }
            }
        }

        target = nearestTarget != null ? nearestTarget.transform : null;
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        //MeleeAttack.bugAttack();
    }

}
