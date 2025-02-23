using UnityEngine;

public class SineWave : MonoBehaviour
{
    public Transform arCamera; // Assign this in the Inspector
    public float speed = 1.0f; // Speed of the breathing wave movement
    public float amplitude = 0.5f; // How far the wave moves during inhale/exhale

    private Vector3 startPosition;

    void Start()
    {
        // Initialize the wave's start position relative to the AR Camera
        startPosition = transform.position;
    }

    void Update()
    {
        // Keep the wave 1.5 units in front of the camera
        transform.position = arCamera.position + arCamera.forward * 1.5f;

        // Animate the wave based on a sine wave for breathing effect
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPosition + new Vector3(0, 0, offset);

        // Ensure the wave always faces the camera (correct rotation)
        transform.LookAt(arCamera);
    }
}
