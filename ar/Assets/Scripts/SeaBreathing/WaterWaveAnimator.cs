using UnityEngine;
using TMPro;
using System;

public class WaterWaveAnimator : MonoBehaviour, IBreathingController
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
    private string currentDisplayedText = "";

    private Vector3 startPosition;
    private float timer;
    private int phase = 0;
    private Boolean isPaused = false;
    public TextAnimator textAnimator;

    void Start()
    {
        startPosition = transform.position;
        if (instructionText == null) {
            instructionText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        }
        if (textAnimator == null) { 
            textAnimator= instructionText.GetComponent<TextAnimator>();
        }
        BreathingExercise();
    }

    // Pause breathing when pop up is shown
    public void PauseBreathing()
    {
        isPaused = true;
    }

    // Resume breathing when pop up is dismissed
    public void ResumeBreathing() {
        isPaused = false;
    }

    void Update()
    {
        // Pause breathing exercise is pop up is shown
        if (isPaused) return;

        // Breathing exercise logic
        timer += Time.deltaTime;

        switch (phase)
        {
            case 0: // Inhale
                BreathingExercise();
                MoveWater(inhaleDistance, inhaleDuration);
                if (timer >= inhaleDuration)
                {
                    timer = 0f;
                    phase = 1;
                }
                break;

            case 1: // Hold
                BreathingExercise();
                if (timer >= holdDuration)
                {
                    timer = 0f;
                    phase = 2;
                }
                break;

            case 2: // Exhale
                BreathingExercise();
                MoveWater(-exhaleDistance, exhaleDuration);
                if (timer >= exhaleDuration)
                {
                    timer = 0f;
                    phase = 0;
                    // Communicate breathing cycle is finished to show prompt if 2 minutes have passed
                    BreathingManager.Instance?.NotifyExhaleComplete(); 
                }
                break;
        }
    }

    // Move the water asset based on breathing times
    void MoveWater(float distance, float duration)
    {
        float moveStep = (distance / duration) * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z + moveStep);
    }

    // Change breathing exercise text based on the phase
    void BreathingExercise()
    {
        string label = "";
        float remainingTime = 0f;

        if (phase == 0)
        {
            label = "Inhale";
            remainingTime = inhaleDuration - timer;
        }
        else if (phase == 1)
        {
            label = "Hold";
            remainingTime = holdDuration - timer;
        }
        else if (phase == 2)
        {
            label = "Exhale";
            remainingTime = exhaleDuration - timer;
        }

        string newText = $"{label}\n{Mathf.Ceil(remainingTime)}";

        // Only update if the text has changed
        if (currentDisplayedText != newText)
        {
            currentDisplayedText = newText;

            if (textAnimator != null)
                textAnimator.FadeToText(newText);
            else
                instructionText.text = newText;
        }
    }

}
