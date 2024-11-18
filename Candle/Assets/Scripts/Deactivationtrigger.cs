using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationTrigger : MonoBehaviour
{
    public Component componentToDeactivate; // The component to deactivate
    public float activationPeriod = 5f; // Time period after which the component is reactivated
    public bool useTimer = true; // Boolean to enable or disable the timer

    private InteractionController interactionController; // Reference to the InteractionController
    private bool isDeactivating = false; // Flag to prevent multiple coroutines

    // Start is called before the first frame update
    void Start()
    {
        interactionController = GetComponent<InteractionController>(); // Get the InteractionController component
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionController != null && interactionController.actionTriggered && !isDeactivating)
        {
            StartCoroutine(DeactivateAndReactivateComponent());
        }
    }

    private IEnumerator DeactivateAndReactivateComponent()
    {
        if (componentToDeactivate != null)
        {
            isDeactivating = true; // Set the flag to prevent multiple coroutines

            // Deactivate the component
            if (componentToDeactivate is Behaviour)
            {
                ((Behaviour)componentToDeactivate).enabled = false;
            }
            else if (componentToDeactivate is Collider)
            {
                ((Collider)componentToDeactivate).enabled = false;
            }
            else if (componentToDeactivate is Renderer)
            {
                ((Renderer)componentToDeactivate).enabled = false;
            }

            if (useTimer)
            {
                yield return new WaitForSeconds(activationPeriod); // Wait for the activation period

                // Reactivate the component
                if (componentToDeactivate is Behaviour)
                {
                    ((Behaviour)componentToDeactivate).enabled = true;
                }
                else if (componentToDeactivate is Collider)
                {
                    ((Collider)componentToDeactivate).enabled = true;
                }
                else if (componentToDeactivate is Renderer)
                {
                    ((Renderer)componentToDeactivate).enabled = true;
                }
            }

            interactionController.actionTriggered = false; // Reset the actionTriggered variable
            isDeactivating = false; // Reset the flag
        }
    }
}
