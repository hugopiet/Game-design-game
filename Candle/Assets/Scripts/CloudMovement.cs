using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 2f; // Base speed to move the GameObject to the right
    public float speedVariation = 0.5f; // Range for speed variation
    public float pushForce = 3f; // Amount to push the other GameObject to the right on trigger enter
    public float despawnX = 10f; // X coordinate at which the GameObject will be despawned

    public Rigidbody2D Playerrb; // Reference to the Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        // Apply random variation to the speed
        speed += Random.Range(-speedVariation, speedVariation);
        if (Playerrb == null)
        {
            Playerrb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
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
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with " + other.name);
        Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pushing " + other.name);
            // Push the other GameObject a little to the right by modifying its position
            Playerrb.AddForce(new Vector3(pushForce, 0, 0), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("No Rigidbody2D found on " + other.name + " or it's not a Player");
        }
    }
}
