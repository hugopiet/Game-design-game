using UnityEngine;


//the idea is that the player has to get the balloon by activating 
//gnist. the balloon will then come to the player, and the player can
//then trigger the action by interacting withe the balloon.
//And then fly away.
public class PlayerBalloonController : MonoBehaviour
{
    private InteractionController interactionController;
    private CharacterController2D cc2D;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private bool underGnist = false;
    private Sprite originalSprite;
    public Sprite flappySprite;

    public SpriteRenderer spriteRenderer;

    [Header("Flappy Mode Settings")]
    public float flapForce = 5f;             // Upward impulse force
    public float reducedGravityScale = 0.5f; // Reduced gravity during FlappyMode
    private float originalGravityScale;      // To store the original gravity scale
    public float breezeForce = 2f; 
    void Start()
    {    
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        originalSprite = spriteRenderer.sprite; // Store the original sprite
        
        cc2D = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        originalGravityScale = rb.gravityScale; // Store original gravity scale
    }

    void Update()
    {
        // Check if under Gnist and action is triggered
        if (underGnist && interactionController != null && interactionController.actionTriggered)
        {
            spriteRenderer.sprite = flappySprite; // Change sprite to flappySprite
            cc2D.enabled = false; // Disable default controller
            playerMovement.enabled = false; // Disable default player movement

            FlappyMode();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalloonZone"))
        {
            interactionController = other.GetComponent<InteractionController>();
            rb.gravityScale = reducedGravityScale; // Reduce gravity
        }
        if (other.CompareTag("Gnist"))
        {
            underGnist = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BalloonZone"))
        {
            spriteRenderer.sprite = originalSprite; // Reset sprite

            playerMovement.enabled = true; // Re-enable default player movement 
            cc2D.enabled = true; // Re-enable default controller
            interactionController = null;
            rb.gravityScale = originalGravityScale; // Reset gravity
        }
        if (other.CompareTag("Gnist"))
        {
            underGnist = false;
        }
    }

    void FlappyMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spriteRenderer.sprite = flappySprite;
            // Apply upward force
            rb.AddForce(new Vector2(0, flapForce), ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(-breezeForce, rb.velocity.y); // Apply breeze force
    }
}