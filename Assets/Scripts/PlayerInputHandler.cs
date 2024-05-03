using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] PlayerBug bug;
    
    // Update is called once per frame
    void FixedUpdate()
    {   
        
        Vector3 input = Vector3.zero;
        float direction = 0f;

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ){
            direction += 1;
            bug.rotateBug(1);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            direction -= 1;
            bug.rotateBug(-1);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ){
            //if(bug.rotAngle > 0) && 
            input = bug.transform.up;
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            input = -bug.transform.up;
        }

        if(Input.GetKey(KeyCode.E) || Input.GetMouseButton(0)){
            //bug.bugAttack();
        } 


        bug.moveBug(input);
    
    }
}
