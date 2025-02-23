using UnityEngine;

public class FoamAnimator : MonoBehaviour
{
    public Material foamMaterial;  // The material of the foam object
    public float foamSpeed = 0.5f; // Speed at which the foam texture moves
    public float foamScale = 1.0f; // Scale of the foam texture movement

    private Vector2 foamOffset;

    void Update()
    {
        // Update foam texture offset using time-based animation
        foamOffset = new Vector2(Mathf.Sin(Time.time * foamSpeed) * foamScale, Mathf.Cos(Time.time * foamSpeed) * foamScale);

        // Apply the offset to the foam material's texture
        foamMaterial.SetTextureOffset("_MainTex", foamOffset); // _MainTex is the default texture property name
    }
}
