using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToBottomRight : MonoBehaviour
{
    public RectTransform uiCanvas; // Reference to the UI Canvas
    public bool useAnimation = true; // Boolean to enable or disable animation
    public float animationDuration = 1f; // Duration of the animation
    public Vector2 offset = new Vector2(-30, 30); // Offset to adjust the position

    private InteractionController interactionController; // Reference to the InteractionController
    private bool isMoving = false; // Flag to prevent multiple coroutines
    private RectTransform rectTransform; // Cached RectTransform
    private SpriteRenderer spriteRenderer; // SpriteRenderer for fading out
    private Image image; // Image component for fading in

    // Start is called before the first frame update
    void Start()
    {
        interactionController = GetComponent<InteractionController>(); // Get the InteractionController component
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        image = GetComponent<Image>(); // Get the Image component

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component is required.");
        }

        if (uiCanvas == null)
        {
            Debug.LogError("UI Canvas RectTransform is not assigned.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is required for fading out.");
        }

        if (image == null)
        {
            Debug.LogError("Image component is required for fading in.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionController != null && interactionController.actionTriggered && !isMoving)
        {
            StartCoroutine(AnimateAndMoveToBottomRight());
        }
    }

    private IEnumerator AnimateAndMoveToBottomRight()
    {
        if (uiCanvas != null && rectTransform != null && spriteRenderer != null && image != null)
        {
            isMoving = true; // Set the flag to prevent multiple coroutines

            Color startColor = spriteRenderer.color; // Define startColor here

            if (useAnimation)
            {
                Vector2 startPosition = rectTransform.anchoredPosition;
                Vector2 bouncePosition = startPosition + new Vector2(0, 5); // Bounce up
                Vector3 startScale = rectTransform.localScale;
                Vector3 endScale = startScale * 2f; // Grow a bit
                float elapsedTime = 0f;

                // Bounce up and grow
                while (elapsedTime < animationDuration / 2)
                {
                    rectTransform.anchoredPosition = Vector2.Lerp(startPosition, bouncePosition, elapsedTime / (animationDuration / 2));
                    rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / (animationDuration / 2));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Fade away
                elapsedTime = 0f;
                Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
                while (elapsedTime < animationDuration / 2)
                {
                    spriteRenderer.color = Color.Lerp(startColor, endColor, elapsedTime / (animationDuration / 2));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Reset position, scale, and alpha
                rectTransform.anchoredPosition = startPosition;
                rectTransform.localScale = startScale;
                spriteRenderer.color = startColor;
            }

            // Set the parent of the GameObject to the canvas
            rectTransform.SetParent(uiCanvas, false);
            Debug.Log("Parent set to UI Canvas");

            // Set the anchor presets to bottom right
            rectTransform.anchorMin = new Vector2(1, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(1, 0);
            Debug.Log("Anchor set to bottom right");

            // Calculate the bottom right position relative to the canvas with offset
            Vector2 targetPosition = new Vector2(offset.x, offset.y);
            Debug.Log($"Target Position with Offset: {targetPosition}");

            // Set the final position
            rectTransform.anchoredPosition = targetPosition;
            Debug.Log($"Final Position: {rectTransform.anchoredPosition}");

            // Fade in using the Image component
            float fadeInDuration = animationDuration / 2;
            float fadeInElapsedTime = 0f;
            Color invisibleColor = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.color = invisibleColor;

            while (fadeInElapsedTime < fadeInDuration)
            {
                image.color = Color.Lerp(invisibleColor, startColor, fadeInElapsedTime / fadeInDuration);
                fadeInElapsedTime += Time.deltaTime;
                yield return null;
            }

            image.color = startColor; // Ensure the final color is fully visible

            interactionController.actionTriggered = false; // Reset the actionTriggered variable
            isMoving = false; // Reset the flag
        }
    }
}
