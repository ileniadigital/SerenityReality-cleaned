using UnityEngine;

public class BreathingWave : MonoBehaviour
{
    public float speed = 1.0f;
    public float amplitude = 0.5f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPosition + new Vector3(0, 0, offset);
    }
}
