using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkGuide : MonoBehaviour
{
    public GameObject particleEmitter; // Reference to the particle emitter
    public Transform ropesParent; // Reference to the parent object of all ropes
    public float moveSpeed = 5f; // Speed of the particle emitter

    private bool isActivated = false;
    private Vector3 initialPosition; // Initial position of the particle emitter
    private InteractionController interactionController; // Reference to the InteractionController

    // Start is called before the first frame update
    void Start()
    {
        particleEmitter.SetActive(false); // Initially deactivate the particle emitter
        initialPosition = particleEmitter.transform.position; // Store the initial position
        interactionController = GetComponent<InteractionController>(); // Get the InteractionController component
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the interaction is triggered
        if (interactionController != null && interactionController.actionTriggered && !isActivated)
        {
            ActivateParticleEmitter();
        }
    }

    private void ActivateParticleEmitter()
    {
        if (!isActivated)
        {
            isActivated = true;
            particleEmitter.SetActive(true); // Activate the particle emitter
            StartCoroutine(MoveAlongRopes());
        }
    }

    private IEnumerator MoveAlongRopes()
    {
        foreach (Transform rope in ropesParent)
        {
            List<Transform> pathPoints = new List<Transform>();

            foreach (Transform bone in rope)
            {
                pathPoints.Add(bone);
            }

            // Sort the pathPoints based on their distance from the initial position of the particle emitter
            pathPoints.Sort((a, b) => Vector3.Distance(particleEmitter.transform.position, a.position).CompareTo(Vector3.Distance(particleEmitter.transform.position, b.position)));

            for (int i = 0; i < pathPoints.Count; i++)
            {
                while (Vector3.Distance(particleEmitter.transform.position, pathPoints[i].position) > 0.1f)
                {
                    particleEmitter.transform.position = Vector3.MoveTowards(particleEmitter.transform.position, pathPoints[i].position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        particleEmitter.SetActive(false); // Deactivate the particle emitter after reaching the end
        particleEmitter.transform.position = initialPosition; // Reset the position to the initial position
        isActivated = false; // Reset isActivated to false
    }
}
