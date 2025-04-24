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

    private IEnumerator BreathingRoutine()
    {
        while (isBreathing)
        {
            yield return StartCoroutine(BreathingCycle());
        }
    }

    private IEnumerator BreathingCycle()
    {
        yield return UpdateBreathPhase("Inhale", minScale, maxScale, inhaleTime);
        yield return UpdateBreathPhase("Hold", maxScale, maxScale, holdTime);
        yield return UpdateBreathPhase("Exhale", maxScale, minScale, exhaleTime);
    }

    private IEnumerator UpdateBreathPhase(string phase, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            int timeLeft = Mathf.CeilToInt(duration - elapsedTime);
            breathText.text = $"{phase}\n{timeLeft}";
            breathingCircle.rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        breathText.text = $"{phase}\n1";
        breathingCircle.rectTransform.localScale = endScale;
        yield return new WaitForSeconds(1f);
    }
}
