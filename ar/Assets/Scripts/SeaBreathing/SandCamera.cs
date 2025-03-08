using UnityEngine;

public class SandCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceBehindWater = 5f;

    void LateUpdate()
    {
        Vector3 newPosition = cameraTransform.position;
        newPosition.z -= Mathf.Abs(distanceBehindWater); // Keep it behind the water
        transform.position = newPosition;
    }
}
