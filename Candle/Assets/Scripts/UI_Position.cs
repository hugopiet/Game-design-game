using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Position : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    private Vector3 offset; // Offset between the object and the camera

    // Start is called before the first frame update
    void Start()
    {
        // Calculate and store the offset
        offset = transform.position - cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the object's position based on the camera's position and the offset
        transform.position = cameraTransform.position + offset;
    }
}
