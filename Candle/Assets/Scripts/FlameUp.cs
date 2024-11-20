using UnityEngine;
using Cinemachine;

public class FlameUp : MonoBehaviour
{
    // Reference to the Animator component
    private Animator gnist;

    public bool flameUp = false;
    public bool flamedUpShort = false;
    public Gnistfollow gnistfollow;
    public ParticleSystem flameParticleSystem; // Reference to the ParticleSystem
    public CinemachineImpulseSource impulseSource; // Reference to the CinemachineImpulseSource

    void Start()
    {
        // Get the Animator component on the same GameObject
        gnist = GetComponent<Animator>();

        // Find the ParticleSystem component with the "FlameUp" tag in the child objects
        if (flameParticleSystem == null)
        {
            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.CompareTag("FlameUp"))
                {
                    flameParticleSystem = ps;
                    break;
                }
            }

            if (flameParticleSystem == null)
            {
                Debug.LogError("ParticleSystem with 'FlameUp' tag not found in children!");
            }
        }

        // Get the CinemachineImpulseSource component on the same GameObject
        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
            if (impulseSource == null)
            {
                Debug.LogError("CinemachineImpulseSource component not found!");
            }
        }
    }

    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed");
            flamedUpShort = true;
            // Check if gnist is in the Wait state
            if (gnistfollow.currentState == Gnistfollow.State.Wait)
            {
                Debug.Log("Gnist is in Wait state, can flame up");
                flameUp = !flameUp;
            }
            else
            {
                Debug.Log("Gnist is not in Wait state, cannot flame up");
                return;
            }

            // Call the OnFlameUp method to update the animator parameter
            OnFlameUp(flameUp);
        }
    }

    public void OnFlameUp(bool isFlameUp)
    {
        // Set the trigger parameter to activate the flameUp animation
        gnist.SetBool("FlameUp", isFlameUp);

        // Trigger the particle system
        if (flameParticleSystem != null)
        {
            if (isFlameUp)
            {
                flameParticleSystem.Play();
                 if (impulseSource != null && flamedUpShort)
                    {
                        impulseSource.GenerateImpulse();
                        flamedUpShort = false;
                    }
            }
            else
            {
                flameParticleSystem.Stop();
            }
        }

        // Trigger the camera shake
       
    }
}