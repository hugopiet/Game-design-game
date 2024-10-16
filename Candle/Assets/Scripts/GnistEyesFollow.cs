using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnistEyesFollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Transform gnist; // Reference to the gnist sprite
    public float radius = 1.0f; // Distance from the center of gnist
    public float heightOffset = 0.5f; // Height offset from the center of gnist
    public Transform eyes; // Reference to the eyes object

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from gnist to the player
        Vector3 direction = (player.position - gnist.position).normalized;

        // Calculate the new position for the eyes relative to gnist
        Vector3 eyesPosition = gnist.position + direction * radius;
        eyesPosition.y = gnist.position.y + heightOffset;

        // Set the position of the eyes
        eyes.position = eyesPosition;
    }
}