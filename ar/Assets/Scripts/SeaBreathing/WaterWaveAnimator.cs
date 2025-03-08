using UnityEngine;
using TMPro;

public class WaterWaveAnimator : MonoBehaviour
{
    [Header("Wave Movement")]
    public float inhaleDistance = 1f;  // Moves the water forward
    public float exhaleDistance = 1f;  // Moves the water backward
    public float moveSpeed = 1f;

    [Header("Breathing Timings (seconds)")]
    public float inhaleDuration = 4f;
    public float holdDuration = 7f;
    public float exhaleDuration = 8f;

    [Header("Guiding text")]
    public TextMeshProUGUI instructionText;

    private Vector3 startPosition;
    private float timer;
    private int phase = 0;

    void Start()
    {
        startPosition = transform.position;
        if (instructionText == null) {
            instructionText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        }
        BreathingExercise();
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (phase)
        {
            case 0: // Inhale
                MoveWater(inhaleDistance, inhaleDuration);
                BreathingExercise();
                if (timer >= inhaleDuration)
                {
                    timer = 0;
                    phase = 1;
                }
                break;

            case 1: // Hold
                BreathingExercise();
                if (timer >= holdDuration)
                {
                    timer = 0;
                    phase = 2;
                }
                break;

            case 2: // Exhale
                MoveWater(-exhaleDistance, exhaleDuration);
                BreathingExercise();
                if (timer >= exhaleDuration)
                {
                    timer = 0;
                    phase = 0;
                }
                break;
        }
    }

    void MoveWater(float distance, float duration)
    {
        float moveStep = (distance / duration) * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z + moveStep);
    }

    void BreathingExercise()
    {
        if (phase == 0) // Inhale
        {
            float remainingTime = inhaleDuration - timer;
            instructionText.text = "Inhale\n " + Mathf.Ceil(remainingTime).ToString();
        }
        else if (phase == 1) // Hold
        {
            float remainingTime = holdDuration - timer;
            instructionText.text = "Hold\n " + Mathf.Ceil(remainingTime).ToString();
        }
        else if (phase == 2) // Exhale
        {
            float remainingTime = exhaleDuration - timer;
            instructionText.text = "Exhale\n " + Mathf.Ceil(remainingTime).ToString();
        }
    }
}
