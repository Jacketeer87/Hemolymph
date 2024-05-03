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

        atkTimer += Time.deltaTime;
        if(atkTimer >= atkDuration){
            atkTimer = 0;
            melee.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take damage
        if(collision.GetComponent<Health>() != null){
            if(collision != self){
                collision.GetComponent<Health>().takeDamage(damage); //damage
            }
            //Check collision is not player/thing creating attack
        }
    }
}
