using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Ensure the Rigidbody2D is initially kinematic
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Turn off the PolygonCollider2D
            polygonCollider.enabled = false;

            // Make the Rigidbody2D non-kinematic to apply gravity
            rb.isKinematic = false;
        }
    }
}
