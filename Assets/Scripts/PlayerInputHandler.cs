using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] PlayerBug bug;
    [SerializeField] MeleeAttack melee;
    ProjectileThrower thrower;
    
    void Start(){
        thrower = bug.GetComponent<ProjectileThrower>();
    }
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
            melee.bugAttack();
        }

        if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1)){
            float rotAngleInDegrees = bug.rotAngle;
            Vector3 launchDirection = AngleToDirection(rotAngleInDegrees);
            thrower.Launch(launchDirection);
        } 


        bug.moveBug(input);
    
    }

    Vector3 AngleToDirection(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad; // Convert degrees to radians
        // Calculate directional vector components based on angle
        float x = Mathf.Sin(angleInRadians);
        float z = Mathf.Cos(angleInRadians);
        return new Vector3(x, 0, z); // y is 0 since we're assuming movement on the x-z plane
    }
}
