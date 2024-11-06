using System;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnistStats : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public HealthBar healthBar; // Reference to the HealthBar instance
    public FlameUp flameUp;
    public Gnistfollow gnistfollow;

    void Start()
    {
        currentStamina = maxStamina;
        healthBar.SetMaxStamina(maxStamina); // Use the instance reference
    }

    void Update()
    {
        Debug.Log("FlameUp: " + flameUp.flameUp);
        if (flameUp.flameUp )
        {
            // Deplete stamina by 10 per second
            DepleteStamina(10f * Time.deltaTime);
        }
        else
        {
            // Regenerate stamina by 5 per second
            RegenerateStamina(5f * Time.deltaTime);
        }

        // Update the health bar using the instance reference
        healthBar.SetStamina(currentStamina);
    }

    public void RegenerateStamina(float amount)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += amount;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    public void DepleteStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }
}
