using UnityEngine;

public class Gnistfollow_level_0 : MonoBehaviour
{
    public Transform player;
    public float radius = 5.0f;
    public float speed = 2.0f;
    public float slowingDistance = 5.0f;

    public enum State { Follow, Wait }
    public State currentState = State.Wait;
    private FootstepController footstepController;

    void Start()
    {
        Debug.Log("Gnist started. Initial state: " + currentState);
        footstepController = GetComponentInChildren<FootstepController>();
        
        if (player == null)
        {
            Debug.LogError("Player reference not set in Gnistfollow_level_0!");
        }
        else
        {
            Debug.Log("Player reference is set correctly");
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Follow:
                FollowPlayer();
                break;
            case State.Wait:
                // Empty state - Gnist just stays in place
                break;
        }
    }

    public void SetFollowState()
    {
        Debug.Log($"SetFollowState called. Previous state: {currentState}");
        currentState = State.Follow;
        Debug.Log($"New state: {currentState}");
    }

    void FollowPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if ((distance - radius) > 0.1f)
        {
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            float adjustedSpeed = speed;
            
            if (distance < radius + slowingDistance)
            {
                adjustedSpeed = speed * (distance - radius) / slowingDistance;
            }

            transform.position += (Vector3)(direction * adjustedSpeed * Time.deltaTime);
            footstepController.StartWalking();
        }
        else
        {
            footstepController.StopWalking();
        }
    }
}