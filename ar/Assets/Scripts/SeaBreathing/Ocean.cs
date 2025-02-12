using UnityEngine;

public class OceanFollowXR : MonoBehaviour
{
    public Transform xrCamera;  // Drag XR Camera here
    public float distanceFromCamera = 1.5f;  // Distance forward
    public float heightOffset = -1.0f;  // Height below camera
    public float waveMovementStrength = 0.2f; // How much water moves with breathing
    private float waveTime;

    void Update()
    {
        if (xrCamera == null) return;

        // Position the ocean in front of the camera but keep it flat
        Vector3 forwardDirection = new Vector3(xrCamera.forward.x, 0, xrCamera.forward.z).normalized;
        Vector3 newPosition = xrCamera.position + forwardDirection * distanceFromCamera;
        newPosition.y = xrCamera.position.y + heightOffset;  // Keep it below the camera

        // Apply position
        transform.position = newPosition;

        // Keep the plane always facing the camera
        transform.LookAt(new Vector3(xrCamera.position.x, transform.position.y, xrCamera.position.z));

        // Add wave-like breathing effect
        waveTime += Time.deltaTime;
        float waveOffset = Mathf.Sin(waveTime) * waveMovementStrength;
        transform.position += transform.forward * waveOffset * Time.deltaTime;
    }
}
