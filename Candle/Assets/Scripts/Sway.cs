using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmplitude = 2.0f; // Amplitude of the sway motion
    public float swayFrequency = 1.0f; // Frequency of the sway motion
    public float curveAmplitude = 0.5f; // Amplitude of the downward curve
    public float startOffset = 0.0f; // Offset for the start time

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float sway = Mathf.Sin(Time.time * swayFrequency + startOffset) * swayAmplitude;
        float curve = Mathf.Abs(Mathf.Cos(Time.time * swayFrequency + startOffset) * curveAmplitude);
        transform.position = startPosition + new Vector3(sway, -curve, 0);
    }
}
