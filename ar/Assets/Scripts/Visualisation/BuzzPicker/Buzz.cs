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
            Vector3 buzzOffset= Random.insideUnitSphere * buzzSpeed *0.01f;
            transform.position = originalPosition + buzzOffset;
        }
    }

    public void SetBuzzSpeed(float speed)
    {
        buzzSpeed = speed;
        isBuzz = speed > 0;
    }

    public float GetBuzzSpeed() 
    {
        return buzzSpeed;
    }
}
