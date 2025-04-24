using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BreathingExercise : MonoBehaviour
{
    [SerializeField] private Image breathingCircle;
    [SerializeField] private TextMeshProUGUI breathText;
    [SerializeField] private float inhaleTime = 5f;
    [SerializeField] private float holdTime = 5f;
    [SerializeField] private float exhaleTime = 5f;
    [SerializeField] private Vector3 minScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f);

    private bool breathingStarted = false;
    private bool isBreathing = false;
    private Coroutine breathingLoop;

    private void Start()
    {
        breathingCircle.gameObject.SetActive(false);
        breathText.gameObject.SetActive(false);
    }

    public void StartBreathing()
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
        while (breaths<3)
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
