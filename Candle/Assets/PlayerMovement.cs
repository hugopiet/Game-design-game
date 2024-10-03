using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public CharacterController2D controller; 
    // Start is called before the first frame update
    public float runSpeed = 40f;
    float horizontalMove = 0f; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        
    }
    
    void FixedUpdate(){
        // move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

}
