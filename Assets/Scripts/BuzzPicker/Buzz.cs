using UnityEngine;

public class Buzz : MonoBehaviour
{
    private float buzzSpeed = 0f;
    private bool isBuzz = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isBuzz)
        {
            transform.position = originalPosition + Random.insideUnitSphere * buzzSpeed * 0.01f;
        }
    }

    public void SetBuzzSpeed(float speed)
    {
        buzzSpeed = speed;
        isBuzz = speed > 0;
    }

    public void StopBuzz()
    {
        isBuzz = false;
        transform.position = originalPosition; // Reset to original position
    }
}
