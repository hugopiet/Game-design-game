using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Information,
    Action
}

public class InteractionController : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 8f;
    public InteractionType interactionType;
    public string informationText = "Default information text";
    public bool actionTriggered = false;
    
    // References
    private SpriteRenderer sprite;
    private Color originalColor;
    private ParticleSystem highlightParticles;
    private bool isInRange = false;

    [Header("Visual Feedback")]
    public bool useColorHighlight = true;
    public bool useParticles = true;
    public float highlightIntensity = 0.9f;
    public float ParticleRadius = 10.5f;

    private void Start()
    {
        // Get components
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        // Setup particles if needed
        if (useParticles)
        {
            SetupParticleSystem();
        }
//!!!!!!!!!
        //highlightParticles.Play();
    }

    private void Update()
    {
        CheckDistance();
        HandleInteraction();
    
    // Add this to handle closing the info bubble
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        InfoBubbleManager.Instance.HideInfoBubble();
    }
    }



    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool wasInRange = isInRange;
        isInRange = distance <= interactionDistance;

        // Handle range enter/exit
        if (isInRange != wasInRange)
        {
            if (isInRange)
            {
                OnEnterRange();
            }
            else
            {
                OnExitRange();
            }
        }
    }

    private void HandleInteraction()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.I))
        {
            switch (interactionType)
            {
                case InteractionType.Information:
                    ShowInformation();
                    break;
                case InteractionType.Action:
                    TriggerAction();
                    break;
            }
        }
    }

    private void OnEnterRange()
    {
        if (useColorHighlight)
        {
            // Brighten the sprite
            Color highlightColor = originalColor * highlightIntensity;
            highlightColor.a = originalColor.a; // Preserve original alpha
            sprite.color = highlightColor;
            Debug.Log("triggered color change");

        }

        if (useParticles && highlightParticles != null)
        {
            highlightParticles.Play();
            Debug.Log("triggered particles");
        }
    }

    private void OnExitRange()
    {
        if (useColorHighlight)
        {
            sprite.color = originalColor;
        }

        if (useParticles && highlightParticles != null)
        {
            highlightParticles.Stop();
        }
        actionTriggered = false;
    }

    private void ShowInformation()
    {
        //Debug.Log($"Showing information: {informationText}");
        
        InfoBubbleManager.Instance.ShowInfoBubble(informationText);
    }

    private void TriggerAction()
    {
        actionTriggered = !actionTriggered; // Toggle the action state
        Debug.Log($"Action triggered: {actionTriggered}");
    }

    private void SetupParticleSystem()
    {
     GameObject particleObj = new GameObject("HighlightParticles");
    particleObj.transform.parent = transform;
    particleObj.transform.localPosition = Vector3.zero;
    
    highlightParticles = particleObj.AddComponent<ParticleSystem>();
    var renderer = particleObj.GetComponent<ParticleSystemRenderer>();
    renderer.material = new Material(Shader.Find("Sprites/Default"));

  var main = highlightParticles.main;
    main.loop = true;
    main.startSize = 0.2f;
    main.startSpeed = 1f;
    main.startLifetime = 1f;
    
    var emission = highlightParticles.emission;
    emission.rateOverTime = 10;

    var shape = highlightParticles.shape;
    shape.shapeType = ParticleSystemShapeType.Circle;
    shape.radius = ParticleRadius;

    highlightParticles.Stop();
}
}