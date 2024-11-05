using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameUp : MonoBehaviour
{
    // Reference to the Animator component
    private Animator gnist;

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
            // Trigger the animation using the specific trigger parameter
            gnist.SetTrigger("Trigger Flame Up");
        }
    }
}
