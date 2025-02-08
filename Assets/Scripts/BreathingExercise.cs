using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreathingExercise : MonoBehaviour
{
    [SerializeField] private Image breathingCircle;
    [SerializeField] private float inhaleTime = 5f;
    [SerializeField] private float holdTime = 5f;
    [SerializeField] private float exhaleTime = 5f;
    [SerializeField] private Vector3 minScale = new Vector3(5f, 5f, 5f);
    [SerializeField] private Vector3 maxScale = new Vector3(100f, 100f, 100f);

    private void Start()
    {
        StartCoroutine(BreathingRoutine());
    }

    private IEnumerator BreathingRoutine()
    {
        for (int i=0; i<3; i++)
        {
            // Inhale (Grow)
            yield return ScaleOverTime(breathingCircle.transform, minScale, maxScale, inhaleTime);

            // Hold
            yield return new WaitForSeconds(holdTime);

            // Exhale (Shrink)
            yield return ScaleOverTime(breathingCircle.transform, maxScale, minScale, exhaleTime);

            // Hold once more
            yield return new WaitForSeconds(holdTime);
        }
    }

    private IEnumerator ScaleOverTime(Transform target, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.localScale = endScale;
    }
}
