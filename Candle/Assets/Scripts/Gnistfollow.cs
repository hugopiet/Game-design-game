using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnistfollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float radius = 5.0f; // Distance threshold
    public float speed = 2.0f; // Movement speed of gnist
    public float slowingDistance = 5.0f; // Distance over which gnist slows down

    // Update is called once per frame
    void Update()
    {
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
}