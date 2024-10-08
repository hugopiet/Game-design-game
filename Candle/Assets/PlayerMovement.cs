using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public CharacterController2D controller; 
    public Animator animator;
    // Start is called before the first frame update
    public float runSpeed = 40f;
    float horizontalMove = 0f; 
    bool jump = false;
    bool crouch = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump") ){
            jump = true;
        }
        if (Input.GetButtonDown("Crouch") ){
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }
    }
    
    void FixedUpdate(){
        // move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

        jump = false;
        Debug.Log(crouch);
    }

}
