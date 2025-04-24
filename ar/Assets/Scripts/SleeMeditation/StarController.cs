using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StarSpawner : MonoBehaviour, IBreathingController
{
    public GameObject starPrefab;
    public RectTransform canvasTransform;
    public Button spawnButton;
    public TextMeshProUGUI instructionText;

    private List<Vector2> existingStars = new List<Vector2>(); // Store placed stars
    private float minDistance = 100f; // Minimum distance between stars

    private float promptTimer = 0f;
    private float promptInterval = 120f;
    private bool isPromptReady = false;
    private bool isPaused = true;

    private TextAnimator textAnimator;

    void Start()
    {
        spawnButton.onClick.RemoveAllListeners();
        spawnButton.onClick.AddListener(SpawnStar);

        StartCoroutine(CheckInTimer());
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

    IEnumerator CheckInTimer()
    {
        while (true)
        { 
            yield return new WaitForSeconds(promptInterval);

            // Wait till the end of the breathing cycle
            while (spawnButton.interactable == false)
            {
                yield return null;
            }
            BreathingManager.Instance?.NotifyExhaleComplete();
        }

    }

    // Create a star at random on the screen
    void SpawnStar()
    {
        spawnButton.interactable = false;

        Vector2 randomPosition = GetRandomPosition();

        if (randomPosition == Vector2.zero)
        {
            spawnButton.interactable = true;
            return;
        }

        GameObject newStar = Instantiate(starPrefab, canvasTransform);
        newStar.GetComponent<RectTransform>().anchoredPosition = randomPosition;
        existingStars.Add(randomPosition);

        StartCoroutine(StarGlowSequence(newStar));
    }

    // Have the star glow
    IEnumerator StarGlowSequence(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();

        // Inhale Phase (4 seconds) - Brighten
        for (int i = 4; i > 0; i--)
        {
            UpdateBreathLabel("Inhale", i);
            float brightness = Mathf.Lerp(1f, 3f, 1 - (i / 4f)); // Overbrighten effect
            starImage.color = new Color(brightness, brightness, brightness, 1f);
            yield return new WaitForSeconds(1f);
        }

        // Hold Breath Phase (7 seconds) - Stay bright
        instructionText.text = "Hold for\n 7";
        //UpdateBreathLabel("Hold for \n", i);
        starImage.color = new Color(3f, 3f, 3f, 1f); // Peak brightness
        yield return new WaitForSeconds(1f);
        for (int i = 6; i > 0; i--)
        {
            //instructionText.text = "Hold for\n " + i;
            yield return new WaitForSeconds(1f);
            UpdateBreathLabel("Hold", i);
        }

        // Exhale Phase (8 seconds) - Dim back
        for (int i = 8; i > 0; i--)
        {
            //instructionText.text = "Exhale for\n " + i;
            UpdateBreathLabel("Exhale", i);
            float brightness = Mathf.Lerp(3f, 0.5f, 1 - (i / 8f)); // Gradually dim
            starImage.color = new Color(brightness, brightness, brightness, 1f);
            yield return new WaitForSeconds(1f);
        }
        // Notify breathing cycle is finished
        if (isPromptReady) {
            BreathingManager.Instance?.NotifyExhaleComplete();
            isPromptReady = false;
            promptTimer = 0f;
        }
        
        instructionText.text = "Place another star";
        spawnButton.interactable = true;
    }

    // Generate random position for the star
    Vector2 GetRandomPosition()
    {
        for (int attempts = 0; attempts < 50; attempts++)
        {
            float halfWidth = canvasTransform.rect.width / 2;
            float halfHeight = canvasTransform.rect.height / 2;

            float starWidth = starPrefab.GetComponent<RectTransform>().sizeDelta.x;
            float starHeight = starPrefab.GetComponent<RectTransform>().sizeDelta.y;

            float minX = -halfWidth + (starWidth / 2);
            float maxX = halfWidth - (starWidth / 2);
            float minY = -halfHeight + (starHeight / 2);
            float maxY = halfHeight - (starHeight / 2);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 newPosition = new Vector2(randomX, randomY);

            bool tooClose = false;
            foreach (Vector2 existingPosition in existingStars)
            {
                if (Vector2.Distance(newPosition, existingPosition) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return newPosition;
        }

        return Vector2.zero; // Return (0,0) if no space is found
    }

    // Update Breathing label with fading animation
    private string currentPhase = "";
    private string lastCount = "";

    void UpdateBreathLabel(string label, int seconds)
    {
        string countStr = seconds.ToString();

        // If label changes → fade
        if (label != currentPhase)
        {
            currentPhase = label;
            lastCount = countStr;

            if (textAnimator != null)
                textAnimator.FadeToText($"{label} for\n {countStr}");
            else
                instructionText.text = $"{label} for\n {countStr}";
        }
        else if (lastCount != countStr)
        {
            lastCount = countStr;

            // No fade for number countdown
            instructionText.text = $"{label} for\n {countStr}";
        }
    }

}
