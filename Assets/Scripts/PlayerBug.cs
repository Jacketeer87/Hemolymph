using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBug : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float speed = 0f;
    [SerializeField] public float rSpeed = 200f;
    
    [SerializeField] public float rotAngle = 0f;
    [SerializeField] public float damage = 1f;


    [Header("Physics")]
    //[SerializeField] layerMask wallMask;
    //[SerializeField] layerMask floorMask;
    //[SerializeField] layerMask element;
    

    [Header("Flavor")]
    [SerializeField] string bugType = "n/a";

    [Header("Tracked Data")]
    [SerializeField] Vector3 start = Vector3.zero;
    float newAngle = 0f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthTracker;



    Rigidbody2D rb;
    SpriteRenderer sr;
    public Transform aim;
    public int startHealth = 10;
    Health playerHealth;
    


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //playerHealth = Health.setHealth(startHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveBug(Vector3 direction){
        rb.velocity = direction*speed;
        
        
    }

    public void rotateBug(float rDirection){
        rotAngle += rDirection * rSpeed * Time.fixedDeltaTime;
        if(rotAngle >= 360f || rotAngle <= -360f)
        {
            rotAngle = 0;
        }
        newAngle = rotAngle;
        Quaternion newRotation = Quaternion.Euler(0f, 0f, rotAngle);
        transform.rotation = newRotation;

        //aim logic
        aim.rotation = newRotation;

    }

    public void webShot(){
        
    }
    
    

    //Keep track of float
    //+speed*Time.deltaTime
    //after adjusting set transform.eulerAngles to 0,0,newAngle

    
}
