using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public float upwardForce = 5f;
    public float distance = 4f;
    public float minAltitude = -26f;
    public float followSpeed = 16f; // Speed to match the player's movement

    bool follow = false;
    [SerializeField] private GnistStats gnistStats;
    [SerializeField] private PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private Collider2D balloonCollider;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero; // For SmoothDamp to work, we need to store the velocity

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        balloonCollider = GetComponent<Collider2D>();

        if (gnistStats == null) Debug.LogError("GnistStats is not assigned in the Inspector!");
        if (balloonCollider == null) Debug.LogError("BalloonManager requires a Collider2D!");
        if (playerMovement == null) Debug.LogError("PlayerMovement reference is missing!");
    }

    private void Update()
    {
        VerticalPosition();

        // If the mouse click is detected inside the balloon's collider, set follow to true
        if (CheckMouseClickInsideBalloon())
        {
            follow = true;
           
        }

        // If follow is true, start following the player's horizontal position
        if (follow)
        {
            Debug.Log("Mouse clicked inside the hot air balloon!");
            FollowPlayerHorizontal();
        }
    }

    private void VerticalPosition()
    {
        if (Input.GetKey(KeyCode.F) && gnistStats != null && gnistStats.currentStamina > 0 && follow)
        {
            rb.velocity = new Vector2(rb.velocity.x, upwardForce);
            gnistStats.DepleteStamina(10f * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        // Prevent balloon from going below the minimum altitude
        if (transform.position.y < minAltitude)
        {
            transform.position = new Vector3(transform.position.x, minAltitude, transform.position.z);
            rb.velocity = Vector2.zero;
        }
    }

    private bool CheckMouseClickInsideBalloon()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // We are working in 2D, so set z to 0

            // Check if the mouse position is inside the collider
            if (balloonCollider != null && balloonCollider.OverlapPoint(mousePosition))
            {
                return true;
            }
        }

        return false;
    }

    private void FollowPlayerHorizontal()
    {
        if (playerMovement == null)
        {
            Debug.LogWarning("PlayerMovement is not assigned!");
            return;
        }

        // // Get the player's current horizontal position
        // float playerX = playerMovement.transform.position.x - distance;

        // // Set the balloon's position horizontally equal to the player's horizontal position
        // // Keep the current vertical position (y), so the balloon stays at the same height
        // transform.position = new Vector3(playerX, transform.position.y, transform.position.z);

        float targetX = playerMovement.transform.position.x - distance;

        // Smoothly move the balloon towards the target X position using SmoothDamp
        float smoothSpeed = 10f; // Adjust this value to control the smooth transition speed
        float smoothTime = 0.1f; // Time it takes to reach the target position

        // Use SmoothDamp for smooth movement
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocity.x, smoothTime, smoothSpeed, Time.deltaTime);

        // Update the balloon's position horizontally
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
