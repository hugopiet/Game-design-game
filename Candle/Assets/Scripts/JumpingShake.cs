using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingShake : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public float rotationAmount = 10f; // Amount of rotation in degrees
    public float shakeSpeed = 1f; // Speed of the shake

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.Isjump)
        {
            transform.Rotate(Vector3.forward, rotationAmount * shakeSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
