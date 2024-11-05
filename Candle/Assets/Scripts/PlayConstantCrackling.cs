using UnityEngine;

public class PlayConstant : MonoBehaviour
{
    public AudioClip[] crackleSounds; // Array to hold footstep sound clips
    public float minTimeBetweenCrackle = 0.3f; // Minimum time between footstep sounds
    public float maxTimeBetweenCrackle = 0.6f; // Maximum time between footstep sounds

    private AudioSource audioSource; // Reference to the Audio Source component
    private float timeSinceLastCrackle; // Time since the last footstep sound

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the Audio Source component
    }

    private void Update()
    {
        // Check if the player is walking
    
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastCrackle >= Random.Range(minTimeBetweenCrackle, maxTimeBetweenCrackle))
            {
                // Play a random footstep sound from the array
                AudioClip crackleSound = crackleSounds[Random.Range(0, crackleSounds.Length)];
                audioSource.PlayOneShot(crackleSound);

                timeSinceLastCrackle = Time.time; // Update the time since the last footstep sound
            }
        }


}