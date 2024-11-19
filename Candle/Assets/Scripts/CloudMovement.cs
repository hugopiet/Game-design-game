using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 2f; // Base speed to move the GameObject to the right
    public float speedVariation = 0.5f; // Range for speed variation
    public float pushAmount = 1f; // Amount to push the other GameObject to the right on trigger enter
    public float despawnX = 10f; // X coordinate at which the GameObject will be despawned

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Apply random variation to the speed
        speed += Random.Range(-speedVariation, speedVariation);

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject to the right with a constant speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the GameObject has reached the despawnX coordinate
        if (transform.position.x >= despawnX)
        {
            Destroy(gameObject); // Despawn the GameObject
        }
    }

    // Method called when the GameObject enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with " + other.name);
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if (otherRb != null)
        {
            Debug.Log("Pushing " + other.name);
            // Temporarily set the Rigidbody to non-kinematic to apply the force
            bool wasKinematic = otherRb.isKinematic;
            if (wasKinematic)
            {
                otherRb.isKinematic = false;
            }

            // Push the other GameObject a little to the right
            otherRb.AddForce(Vector3.right * pushAmount, ForceMode.Impulse);

            // Restore the kinematic state
            if (wasKinematic)
            {
                otherRb.isKinematic = true;
            }
        }
        else
        {
            Debug.Log("No Rigidbody found on " + other.name);
        }
    }
}
