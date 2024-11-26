using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public InteractionController interactionController;
    public MoveToBottomRight collectableController;
    public SceneController sceneController;
    public GameObject messageImage; // GameObject for the message image
    public float messageDisplayTime = 2f; // Time to display the message
    public int levelToLoad = 1; // Level index to load

    void Start()
    {
        if (interactionController == null || collectableController == null || sceneController == null || messageImage == null)
        {
            Debug.LogError("Please assign all required references in the inspector.");
            return;
        }

        // Ensure the message image is initially disabled
        messageImage.SetActive(false);
    }

    void Update()
    {
        if (interactionController.actionTriggered)
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        if (MoveToBottomRight.LevelKey)
        {
            sceneController.LoadSpecificLevel(levelToLoad);
        }
        else
        {
            ShowMessage();
        }
    }

    private void ShowMessage()
    {
        // Enable the message image
        messageImage.SetActive(true);

        // Start a coroutine to hide the message after a delay
        StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);

        // Disable the message image
        messageImage.SetActive(false);
    }
}
