using UnityEngine;

public class FoamAnimator : MonoBehaviour
{
    [Header("Materials")]
    public Material foamMaterial; // Assign your foam material

    [Header("Water Sync")]
    public WaterWaveAnimator waterWaveAnimator; // Drag your water animator here

    [Header("Foam Movement")]
    public float foamSpeedMultiplier = 1f; // Adjust how fast foam moves compared to water
    public Vector2 tiling = new Vector2(500f, 500f); // Control how zoomed-in the foam texture looks

    private Vector2 foamOffset;
    private Vector3 lastWaterPosition;

    void Start()
    {
        if (foamMaterial != null)
        {
            foamMaterial.SetTextureScale("_MainTex", tiling);
        }

        if (waterWaveAnimator != null)
        {
            lastWaterPosition = waterWaveAnimator.transform.position;
        }
    }

    void Update()
    {
        if (foamMaterial != null && waterWaveAnimator != null)
        {
            //// Calculate water movement since the last frame
            //Vector3 waterMovement = waterWaveAnimator.transform.position - lastWaterPosition;

            //// Move the foam texture based on water movement
            //foamOffset += new Vector2(waterMovement.x, waterMovement.z) * foamSpeedMultiplier;
            //foamMaterial.SetTextureOffset(" _MainTex", foamOffset);

            //// Store the water's current position for the next frame
            //lastWaterPosition = waterWaveAnimator.transform.position;
            foamOffset += new Vector2(Time.deltaTime * 0.1f, Time.deltaTime * 0.1f);
            foamMaterial.SetTextureOffset("_MainTex", foamOffset);

        }
    }
}
