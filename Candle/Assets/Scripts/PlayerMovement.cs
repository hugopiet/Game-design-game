using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    // Start is called before the first frame update
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public bool Isjump = false;

    bool crouch = false;

    private FootstepController footstepController;

    void Start()
    {
        footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
    }

    private void Awake()
    {
        //footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Isjump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (horizontalMove == 0)
        {
            footstepController.StopWalking();
        }
        else
        {
            footstepController.StartWalking();
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        Isjump = false;
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouched", isCrouching);
    }

    void FixedUpdate()
    {
        // move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
