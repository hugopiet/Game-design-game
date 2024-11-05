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

    private FootstepController footstepController;

    public Camera _mainCamera;

    private Bounds _mainBounds;
    private Vector3 _targetPosition;
    void Start()
    {
        footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect; 

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds(); 
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0f),
            new Vector3(maxX, maxY, 0f)
        );
    }

     private void Awake()
    {
        _mainCamera = Camera.main;
        //footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
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

        if (horizontalMove == 0){
            footstepController.StopWalking();
        }
        else{
            footstepController.StartWalking();
        }
    }

    public void OnCrouching (bool isCrouching){
        animator.SetBool("IsCrouched", isCrouching);
    }
    
    void FixedUpdate(){
        // move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

      
        
    }

}
