using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BreathingExercise : MonoBehaviour
{
    [SerializeField] private Image breathingCircle;
    [SerializeField] private TextMeshProUGUI breathText; // Text inside the circle
    [SerializeField] private float inhaleTime = 5f;
    [SerializeField] private float holdTime = 5f;
    [SerializeField] private float exhaleTime = 5f;
    [SerializeField] private Vector3 minScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f);

    private bool breathingStarted = false;
    private void Start()
    {
        breathingCircle.gameObject.SetActive(false);
        breathText.gameObject.SetActive(false);
    }

    public void StartBreathing()
    {
        if (!breathingStarted) {
            breathingStarted = true;
            breathingCircle.gameObject.SetActive(true);
            breathText.gameObject.SetActive(true);

            // Start animation
            StartCoroutine(BreathingRoutine());
        }
    }

    private IEnumerator BreathingRoutine()
    {
        for (int i=0; i<3; i++)
        {
            // Inhale first 
            yield return UpdateBreathPhase("Inhale", minScale, maxScale, inhaleTime);

            // Hold
            yield return UpdateBreathPhase("Hold", maxScale, maxScale, holdTime);

            // Exhale
            yield return UpdateBreathPhase("Exhale", maxScale, minScale, exhaleTime);

            // Hold
            yield return UpdateBreathPhase("Hold", minScale, minScale, holdTime);
        }

        breathingCircle.gameObject.SetActive(false);
        breathText.gameObject.SetActive(false);
    }

    private IEnumerator UpdateBreathPhase(string phase, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Update text inside the circle
            int timeLeft = Mathf.CeilToInt(duration - elapsedTime);
            breathText.text = $"{phase}\n{timeLeft}";

            // Animate the circle size
            breathingCircle.rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values match exactly
        breathText.text = $"{phase}\n1";
        breathingCircle.rectTransform.localScale = endScale;
        yield return new WaitForSeconds(1f);
    }
}
