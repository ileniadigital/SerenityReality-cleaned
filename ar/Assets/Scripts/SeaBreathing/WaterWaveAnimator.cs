// Controls the water wave animation and breathing exercise text
//
// This script is attached to the water GameObject in the scene
using UnityEngine;
using TMPro;
using System;

public class WaterWaveAnimator : MonoBehaviour, IBreathingController
{
    [Header("Wave Movement")]
    public float inhaleDistance = 1f;  // Moves the water forward
    public float exhaleDistance = 1f;  // Moves the water backward
    public float moveSpeed = 1f; // Speed of the water movement

    [Header("Breathing Timings (seconds)")]
    public float inhaleDuration = 4f; // Time to inhale
    public float holdDuration = 7f; // Time to hold breath
    public float exhaleDuration = 8f; // Time to exhale

    [Header("Guiding text")]
    public TextMeshProUGUI instructionText; // TextMeshProUGUI component to show breathing instructions
    private string currentDisplayedText = ""; // Text currently displayed
    private string currentPhaseLabel = ""; // Current phase label (Inhale, Hold, Exhale)
    private string lastCountdown = ""; // Last countdown text displayed

    private Vector3 startPosition; // Initial position of the water GameObject
    private float timer; // Timer to track the duration of each phase
    private int phase = 0; // Current phase of the breathing exercise (0: Inhale, 1: Hold, 2: Exhale)
    private Boolean isPaused = false; // Flag to check if the breathing exercise is paused
    public TextAnimator textAnimator; // TextAnimator component to animate the text

    void Start()
    // Initialise start position of the water
    {
        startPosition = transform.position;
        if (instructionText == null)
        {
            instructionText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        }
        if (textAnimator == null)
        {
            textAnimator = instructionText.GetComponent<TextAnimator>();
        }
        BreathingExercise();
    }

    // Pause breathing when pop up is shown
    public void PauseBreathing()
    {
        isPaused = true;
    }

    // Resume breathing when pop up is dismissed
    public void ResumeBreathing()
    {
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

        string countdownText = Mathf.Ceil(remainingTime).ToString();
        // Fade to new label after each phase ends
        if (label != currentPhaseLabel)
        {
            currentPhaseLabel = label;

            // Animate text
            if (textAnimator != null)
            {
                textAnimator.FadeToText($"{label}\n{countdownText}");
            }
            // If no animator is available, simply set text
            else
            {
                instructionText.text = $"{label}\n{countdownText}";
            }
        }
        else
        {
            // Update countdown only
            if (lastCountdown != countdownText)
            {
                lastCountdown = countdownText;
                // Set text directly
                instructionText.text = $"{currentPhaseLabel}\n{lastCountdown}";
            }
        }
    }

}
