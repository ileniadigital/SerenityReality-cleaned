using UnityEngine;

public class WaterWaveAnimator : MonoBehaviour
{
    public float waveAmplitude = 1f;     // Amplitude of the wave (how far the water moves up/down)
    public float waveSpeed = 1f;         // Speed of the wave motion (how fast it moves)
    public float waveDirection = 1f;     // Direction of the wave (positive for approaching, negative for retreating)

    private Vector3 startPosition;       // Starting position of the water block

    void Start()
    {
        // Store the starting position of the water block
        startPosition = transform.position;
    }

    void Update()
    {
        // Create a smooth wave-like motion using sine function (for smooth oscillation)
        float waveMovement = Mathf.Sin(Time.time * waveSpeed) * waveAmplitude * waveDirection;

        // Update the water block's position to simulate wave motion
        transform.position = new Vector3(startPosition.x, startPosition.y + waveMovement, startPosition.z);
    }
}
