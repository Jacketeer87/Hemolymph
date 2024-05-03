using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //health stored here
    public int currHealth;
    public int MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(int hAmount){
        currHealth = hAmount;

    }

    public void takeDamage(int dAmount){
        currHealth -= dAmount;

        if(currHealth <= 0){
            Destroy(gameObject);
        }
    }




}
