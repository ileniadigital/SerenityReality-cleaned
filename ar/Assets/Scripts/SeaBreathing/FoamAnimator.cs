using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FoamLineAnimator : MonoBehaviour
{
    [Header("Wave Settings")]
    public int pointCount = 100; // Number of points for smooth curve
    public float waveAmplitude = 0.5f; // Height of the wave
    public float waveLength = 2f; // Distance between wave peaks
    public float waveSpeed = 1f; // Speed of wave movement

    private LineRenderer lineRenderer;
    private float timeOffset;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount;
        timeOffset = Random.Range(0f, 100f); // Randomize starting point of wave
    }

    void Update()
    {
        AnimateWave();
    }

    void AnimateWave()
    {
        for (int i = 0; i < pointCount; i++)
        {
            float x = i * (waveLength / pointCount); // Spread points evenly
            float y = Mathf.Sin((x + Time.time * waveSpeed + timeOffset) * Mathf.PI * 2f) * waveAmplitude;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
