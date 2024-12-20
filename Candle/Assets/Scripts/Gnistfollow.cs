using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnistfollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float radius = 5.0f; // Distance threshold
    public float speed = 2.0f; // Movement speed of gnist
    public float slowingDistance = 5.0f; // Distance over which gnist slows down
    public float border = 0.1f; // Buffer distance within the view
    public float staminaRegenRate = 5.0f; // Rate of stamina regeneration
    public float staminaDepleteRate = 5.0f; // Rate of stamina depletion
    public float staminaDepleteRateFlameUp = 10.0f; // Rate of stamina depletion when flameUp is active
    public GnistStats gnistStats; // Reference to the GnistStats script
    
    // New public bool for avoiding being looked at
    public bool avoidBeingLookedAt = false;

    public enum State { Follow, Position, Wait }
    public State currentState = State.Follow;
    private Vector3 targetPosition; // Target position for the Position state
    private Vector3 followTargetPosition;
    private float waitCounter; // Counter for the Wait state

    private FootstepController footstepController;
    public FlameUp flameUp;

    void Start()
    {
        footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
    }


    void Update()
    {
        // Check if gnist is within the camera view
        if (!IsInView())
        {
            currentState = State.Follow;
        }

        switch (currentState)
        {
            case State.Follow:
                FollowPlayer();
                break;
            case State.Position:
                MoveToPosition();
                break;
            case State.Wait:
                WaitState();
                break;
        }

        // Check for "Fire1" input to activate Position state
        if (Input.GetButtonDown("Fire1") && gnistStats.currentStamina > 0)
        {
            Debug.Log("Fire1 input detected");
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Set z to the gnist's z position in world space
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentState = State.Position;
            Debug.Log("Clicked Position: " + targetPosition);
            Debug.Log("State changed to Position");
        }

        // Check for "R" input to activate Follow state
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentState = State.Follow;
            Debug.Log("R key pressed, state changed to Follow");
        }
    }

        void FollowPlayer()
    {
        footstepController.StartWalking();
        // Calculate the distance between gnist and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // If avoiding being looked at, calculate the opposite side position
        if (avoidBeingLookedAt)
        {
            // Determine facing direction using the sign of the x scale
            float facingDirection = Mathf.Sign(player.localScale.x);
            
            // Calculate the opposite direction based on facing direction
            Vector3 oppositeDirection = new Vector3(-facingDirection , 0, 0);
            
            // Set target position on the opposite side of the player
            followTargetPosition = player.position + (oppositeDirection * radius);
            
            distance = Vector3.Distance(transform.position, followTargetPosition) + radius;
            
            // Move towards the opposite side position
            //Vector3 direction = (targetPosition - transform.position).normalized;
            //transform.position += direction * speed * Time.deltaTime;
        }
        else{
            followTargetPosition = player.position;
        }
        
            // Original follow behavior
           // if ((distance - radius) > 0.2f)
            //{
                // Calculate the direction to move towards the player
                Vector3 direction = (followTargetPosition - transform.position).normalized;

                // Calculate the speed based on the distance
                float adjustedSpeed = speed;
                if (distance < radius + slowingDistance)
                {
                    adjustedSpeed = speed * (distance - radius) / slowingDistance;
                }

                if((followTargetPosition - transform.position).magnitude >= (player.position - transform.position).magnitude && avoidBeingLookedAt)
                {
                    adjustedSpeed = speed*5;
                }
                // Move gnist towards the player
                transform.position += direction * adjustedSpeed * Time.deltaTime;
               
    
            //}


            if((distance - radius) < 0.2f)
            {
                footstepController.StopWalking();
            }
        

        // Regenerate stamina when within the follow radius
        if (distance <= radius + slowingDistance)
        {
            gnistStats.RegenerateStamina(staminaRegenRate * Time.deltaTime);
        }
    }

    void MoveToPosition()
    {
        //Debug.Log("Moving to position: " + targetPosition);
        // Calculate the distance between gnist and the target position
        float distance = Vector3.Distance(transform.position, targetPosition);

        // If the distance is greater than a small threshold, move gnist towards the target position
        if (distance > 0.1f)
        {
            // Calculate the direction to move towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Move gnist towards the target position
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // Transition to Wait state when gnist reaches the target position
            currentState = State.Wait;
            Debug.Log("Reached target position, state changed to Wait");
        }
    }

    void WaitState()
    {
        //Debug.Log("Waiting...");
        // Decrease the wait counter
        waitCounter -= Time.deltaTime;
        // Decrease stamina based on the flameUp state
        if (gnistStats.flameUp.flameUp)
        {
            gnistStats.DepleteStamina(staminaDepleteRateFlameUp * Time.deltaTime);
        }
        else
        {
            gnistStats.DepleteStamina(staminaDepleteRate * Time.deltaTime);
        }

        // If the current stamina reaches zero, transition to Follow state
        if (gnistStats.currentStamina <= 0)
        {
            currentState = State.Follow;
            gnistStats.flameUp.flameUp = false; // Reset the flameUp state
            Debug.Log("Stamina depleted, state changed to Follow");
            flameUp.OnFlameUp(false); // Turn off flameUp animation
        }
    }

    bool IsInView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > border && screenPoint.x < 1 - border && screenPoint.y > border && screenPoint.y < 1 - border;
    }
}