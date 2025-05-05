// TextAnimator animates the text in the Sea Breathing Scene
//
// It must be assigned to a TMP_Text component in the scene
using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
    public TMP_Text targetText; // TextMeshPro component to animate
    public float fadeDuration = 0.5f; // Duration of fade animation
    private Coroutine fadeRoutine; // Current fade routine

    // Set text component if null
    void Awake()
    {
        if (targetText == null)
            targetText = GetComponent<TMP_Text>();
    }

    // Start the fading animation
    public void FadeToText(string newText)
    {
        if (targetText.text == newText) return;
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTextRoutine(newText));
    }

    // Fading animation timing
    private IEnumerator FadeTextRoutine(string newText)
    {
        Color originalColor = targetText.color;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        targetText.text = newText;

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Reset color
        targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}
