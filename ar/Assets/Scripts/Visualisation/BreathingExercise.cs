// Handles the Breathing exercise visualisation
//
// It must be attached to the BreathingExercise prefab in the scene
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BreathingExercise : MonoBehaviour
{
    [SerializeField] private Image breathingCircle; // The image that represents the breathing circle
    [SerializeField] private TextMeshProUGUI breathText; // The text that displays the current breathing phase and time left
    [SerializeField] private float inhaleTime = 5f; // The time for inhaling
    [SerializeField] private float holdTime = 5f; // The time for holding the breath
    [SerializeField] private float exhaleTime = 5f; // The time for exhaling
    [SerializeField] private Vector3 minScale = new Vector3(1f, 1f, 1f); // The minimum size of the breathing circle
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f); // The maximum size of the breathing circle

    private bool breathingStarted = false; // Flag to check if breathing has started
    private bool isBreathing = false; // Flag to check if breathing is in progress
    private Coroutine breathingLoop; // Coroutine for breathing loop

    private void Start()
    // Initialise the elements
    {
        breathingCircle.gameObject.SetActive(false);
        breathText.gameObject.SetActive(false);
    }

    public void StartBreathing()
    // Start the breathing exercise
    {
        if (!breathingStarted)
        {
            breathingStarted = true;
            isBreathing = true;

            breathingCircle.gameObject.SetActive(true);
            breathText.gameObject.SetActive(true);

            breathingLoop = StartCoroutine(BreathingRoutine());
        }
    }

    // Stop breathing exercise
    public void StopBreathing()
    {
        isBreathing = false;

        if (breathingLoop != null)
        {
            StopCoroutine(breathingLoop);
        }

        breathingCircle.gameObject.SetActive(false);
        breathText.gameObject.SetActive(false);
    }

    // Start breathing exercises
    private IEnumerator BreathingRoutine()
    {
        int breaths = 0; // Count breaths
        while (breaths < 3)
        {
            yield return StartCoroutine(BreathingCycle());
            breaths++;
        }
        // Stop after 3 breaths
        StopBreathing();
    }

    // Breathing Cycle animation and text
    private IEnumerator BreathingCycle()
    {
        yield return UpdateBreathPhase("Inhale", minScale, maxScale, inhaleTime);
        yield return UpdateBreathPhase("Hold", maxScale, maxScale, holdTime);
        yield return UpdateBreathPhase("Exhale", maxScale, minScale, exhaleTime);
    }

    // Update breathing animation and text
    private IEnumerator UpdateBreathPhase(string phase, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f; // initial time
        while (elapsedTime < duration)
        {
            int timeLeft = Mathf.CeilToInt(duration - elapsedTime);
            breathText.text = $"{phase}\n{timeLeft}"; // Chnage text
            breathingCircle.rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration); // Transform breathing circle
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        breathText.text = $"{phase}\n1";
        breathingCircle.rectTransform.localScale = endScale;
        yield return new WaitForSeconds(1f);
    }
}
