using UnityEngine;

public class WaterWaveAnimator : MonoBehaviour
{
    public Material waterMaterial;    // The water material with the Shader Graph
    public float inhaleHeight = 1.0f; // Maximum wave height (inhale)
    public float exhaleHeight = 0.1f; // Minimum wave height (exhale)
    public float waveSpeed = 1.0f; // Speed at which the waves animate (breathing)

    private float time;

    void Update()
    {
        // Update time with a speed factor
        time += Time.deltaTime * waveSpeed;

        // Create a smooth oscillation between inhale and exhale heights
        float waveHeight = Mathf.Lerp(exhaleHeight, inhaleHeight, (Mathf.Sin(time) + 1.0f) / 2.0f);

        // Pass the animated values to the Shader
        waterMaterial.SetFloat("_WaveHeight", waveHeight);
        waterMaterial.SetFloat("_WaveTime", time);
    }
}
