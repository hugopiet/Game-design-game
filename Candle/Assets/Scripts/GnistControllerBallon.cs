using UnityEngine;

public class GnistControllerBallon : MonoBehaviour
{
    private Gnistfollow gnistFollow;
    private bool isInTriggerZone = false;
    public GnistStats gnistStats;
    private InteractionController interactionController;

    private bool continueThisScript = false;
    [Header("Hover Settings")]
    public Transform player;
    public Transform balloon;           // Reference to BalloonZone
    public float hoverHeight = 3f;
    public float moveSpeed = 5f;
    public float smoothTime = 0.3f;

    private Vector2 targetPosition;
    private Vector2 currentVelocity;

    private enum BalloonState
    {
        HoverMode,
        FlappyMode
    }
    private BalloonState currentState;

    void Start()
    {
        gnistFollow = GetComponent<Gnistfollow>();
        currentState = BalloonState.HoverMode;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (balloon == null)
            balloon = GameObject.FindGameObjectWithTag("BalloonZone").transform;
    }

    void Update()
    {
        if (isInTriggerZone && (gnistStats.flameUp.flameUp || continueThisScript))
        {
            gnistFollow.enabled = false;
            balloon.position = transform.position;
            continueThisScript = true; 

            switch (currentState)
            {
                case BalloonState.HoverMode:
                    HoverOverPlayer();

                    if (interactionController && interactionController.actionTriggered)
                    {
                        currentState = BalloonState.FlappyMode;
                        // FlappyMode behavior to be defined later
                    }
                    break;

                case BalloonState.FlappyMode:
                    // FlappyMode behavior not defined yet
                    targetPosition = new Vector2(player.position.x, player.position.y + hoverHeight);
                    transform.position = targetPosition;

                    break;
            }

            gnistStats.flameUp.flameUp = false;
        }
        else
        {
            gnistFollow.enabled = true;
        }
    }

    void HoverOverPlayer()
    {
        targetPosition = new Vector2(player.position.x, player.position.y + hoverHeight);
        transform.position = Vector2.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime,
            moveSpeed
        );
        balloon.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalloonZone"))
        {
            isInTriggerZone = true;
            interactionController = other.GetComponent<InteractionController>();
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BalloonZone"))
        {
            isInTriggerZone = false;
            interactionController = null;
            currentState = BalloonState.HoverMode;
            gnistFollow.enabled = true;
        }
    }



}