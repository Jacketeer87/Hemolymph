using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject melee;
    
    [SerializeField] float atkDuration = 0.3f;
    [SerializeField] float atkTimer = 0f;
    [SerializeField] int damage;
    [SerializeField] Collider2D self;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bugAttack(){
        melee.SetActive(true);
        //call animator to play attack
        Debug.Log("Hiya");
        atkTimer += Time.deltaTime;
        if(atkTimer >= atkDuration){
            atkTimer = 0;
            melee.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attack applied"+collision.gameObject.name);
        //take damage
        if(collision.GetComponent<Health>() != null){
            Debug.Log("Accessed collision");
            if(collision != self){
                collision.GetComponent<Health>().TakeDamage(damage); //damage
                Debug.Log("Applied damage");
          }
            //Check collision is not player/thing creating attack
        }
    }
}
