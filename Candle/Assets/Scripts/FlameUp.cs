using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameUp : MonoBehaviour
{
    // Reference to the Animator component
    private Animator gnist;

    public bool flameUp = false;

    public Gnistfollow gnistfollow;

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
    }
}
