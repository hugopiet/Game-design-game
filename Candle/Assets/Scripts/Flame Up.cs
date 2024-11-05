using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameUp : MonoBehaviour
{
    // Reference to the Animator component
    private Animator gnist;

    private bool flameUp = false;

    void Start()
    {
        // Get the Animator component on the same GameObject
        gnist = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed");
            // Toggle the flameUp boolean
            flameUp = !flameUp;
            // Call the OnFlameUp method to update the animator parameter
            OnFlameUp(flameUp);
        }
    }

    public void OnFlameUp(bool isFlameUp)
    {
        // Set the trigger parameter to activate the flameUp animation
        gnist.SetBool("FlameUp", isFlameUp);
    }
}
