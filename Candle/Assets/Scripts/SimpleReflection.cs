using UnityEngine;

public class SimpleReflection : MonoBehaviour
{
    [Header("Reflection Settings")]
    public float offset = 1f; // Distance to the side for the mirror reflection
    public Color reflectionColor = new Color(1f, 1f, 1f, 0.5f); // Tint and transparency
    public SpriteMask spriteMask; // Reference to the Sprite Mask (if needed)

    private GameObject reflection;
    private SpriteRenderer reflectionRenderer;
    private SpriteRenderer originalRenderer;

    void Start()
    {
        CreateReflection();
    }

    void Update()
    {
        UpdateReflectionPosition();
    }

    void CreateReflection()
    {
        // Create a reflection GameObject
        reflection = new GameObject("Reflection");
        reflection.transform.SetParent(transform);

        // Copy SpriteRenderer from the original
        originalRenderer = GetComponent<SpriteRenderer>();
        reflectionRenderer = reflection.AddComponent<SpriteRenderer>();
        reflectionRenderer.sprite = originalRenderer.sprite;
        reflectionRenderer.flipY = false; // No vertical flip for wall mirrors
        reflectionRenderer.color = reflectionColor; // Apply transparency/tint
        reflectionRenderer.sortingOrder = originalRenderer.sortingOrder - 1;

        // Assign the reflection sprite to interact with the mask
        if (spriteMask != null)
        {
            reflectionRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

        // Set initial position
        UpdateReflectionPosition();
    }

    void UpdateReflectionPosition()
    {
        // Determine the player's facing direction
        float facingDirection = Mathf.Sign(transform.localScale.x);

        // Update the reflection's local position
        reflection.transform.localPosition = new Vector3(offset * facingDirection, 0, 0);
    }
}
