// Part of the BuzzPicker object, handles the buzz of the object
//
// It must be attached to the buzzing object
using UnityEngine;

public class Buzz : MonoBehaviour
{
    private float buzzSpeed = 0f; // Speed of the buzz
    private bool isBuzz = false; // Tracks buzzing state
    private Vector3 originalPosition; // Original position of the object

    private void Start()
    // Initialise original position of the object
    {
        originalPosition = transform.position;
    }

    private void Update()
    // Update the position of the object based on the buzz speed
    {
        if (isBuzz)
        {
            Vector3 buzzOffset = Random.insideUnitSphere * buzzSpeed * 0.01f;
            transform.position = originalPosition + buzzOffset;
        }
    }

    public void SetBuzzSpeed(float speed)
    // Set the speed of the buzz and update the state
    {
        buzzSpeed = speed;
        isBuzz = speed > 0;
    }

    public float GetBuzzSpeed()
    // Get the current buzz speed
    {
        return buzzSpeed;
    }
}
