using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingContacts : MonoBehaviour
{
    private Transform originalParent;
    private Vector3 lastPlatformPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            lastPlatformPosition = transform.position;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}