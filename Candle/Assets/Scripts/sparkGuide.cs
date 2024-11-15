using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkGuide : MonoBehaviour
{
    public GameObject particleEmitter; // Reference to the particle emitter
    public Transform bonesParent; // Reference to the parent object of the bones
    public float moveSpeed = 5f; // Speed of the particle emitter

    private bool isActivated = false;
    private List<Transform> pathPoints = new List<Transform>(); // List of points defining the path along the wire
    private InteractionController interactionController; // Reference to the InteractionController

    // Start is called before the first frame update
    void Start()
    {
        particleEmitter.SetActive(false); // Initially deactivate the particle emitter
        CollectBoneTransforms(); // Collect the bone transforms
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

    private void CollectBoneTransforms()
    {
        foreach (Transform bone in bonesParent)
        {
            pathPoints.Add(bone);
        }
    }

    private void ActivateParticleEmitter()
    {
        if (!isActivated)
        {
            isActivated = true;
            particleEmitter.SetActive(true); // Activate the particle emitter
            StartCoroutine(MoveAlongPath());
        }
    }

    private IEnumerator MoveAlongPath()
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            while (Vector3.Distance(particleEmitter.transform.position, pathPoints[i].position) > 0.1f)
            {
                particleEmitter.transform.position = Vector3.MoveTowards(particleEmitter.transform.position, pathPoints[i].position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        particleEmitter.SetActive(false); // Deactivate the particle emitter after reaching the end
        isActivated = false; // Reset isActivated to false
    }
}
