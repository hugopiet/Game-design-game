using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnistfollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float radius = 5.0f; // Distance threshold
    public float speed = 2.0f; // Movement speed of gnist
    public float slowingDistance = 5.0f; // Distance over which gnist slows down
    public float waitTime = 5.0f; // Time to wait in the Wait state
    public float border = 0.1f; // Buffer distance within the view

    private enum State { Follow, Position, Wait }
    private State currentState = State.Follow;
    private Vector3 targetPosition; // Target position for the Position state
    private float waitCounter; // Counter for the Wait state

    // Update is called once per frame
    void Update()
    {
        // Check if gnist is within the camera view
        if (!IsInView())
        {
            currentState = State.Follow;
            Debug.Log("Gnist exited camera view, state changed to Follow");
        }

        //Debug.Log("Current State: " + currentState.ToString());
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
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 input detected");
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Set z to the gnist's z position in world space
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentState = State.Position;
            Debug.Log("Clicked Position: " + targetPosition);
            Debug.Log("State changed to Position");
        }
    }

    void FollowPlayer()
    {
        Debug.Log("Following player");
        // Calculate the distance between gnist and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // If the distance is greater than the radius, move gnist towards the player
        if (distance > radius)
        {
            // Calculate the direction to move towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Calculate the speed based on the distance
            float adjustedSpeed = speed;
            if (distance < radius + slowingDistance)
            {
                adjustedSpeed = speed * (distance - radius) / slowingDistance;
            }

            // Move gnist towards the player
            transform.position += direction * adjustedSpeed * Time.deltaTime;
        }
    }

    void MoveToPosition()
    {
        Debug.Log("Moving to position: " + targetPosition);
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
            waitCounter = waitTime; // Initialize the wait counter
            Debug.Log("Reached target position, state changed to Wait");
        }
    }

    void WaitState()
    {
        Debug.Log("Waiting...");
        // Decrease the wait counter
        waitCounter -= Time.deltaTime;

        // If the wait counter reaches zero, transition to Follow state
        if (waitCounter <= 0)
        {
            currentState = State.Follow;
            Debug.Log("Wait time over, state changed to Follow");
        }
    }

    bool IsInView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > border && screenPoint.x < 1 - border && screenPoint.y > border && screenPoint.y < 1 - border;
    }
}