// Add stars to the screen at random positions after pressing the moon button
// Stars will glow and fade out in a breathing pattern
// 
// The script is attached to the star spawner object in the scene
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab; // Prefab for the star object
    public RectTransform canvasTransform; // Reference to the canvas transform for positioning stars
    public Button spawnButton; // Button to spawn stars
    public TextMeshProUGUI instructionText; // TextMeshProUGUI component to show instructions

    private List<Vector2> existingStars = new List<Vector2>(); // Store placed stars
    private float minDistance = 100f; // Minimum distance between stars

    // Handle check-in by number of breaths (6 breaths = 1 check-in = every 2 minutes)
    private int breathCounter = 0; // Counter for the number of breaths completed
    private int BreathsBeforePopUp = 6; // Number of breaths before triggering a pop-up

    private TextAnimator textAnimator; // Reference to the TextAnimator component for fading text

    void Start()
    // Initialise star spawner
    {
        spawnButton.onClick.RemoveAllListeners();
        spawnButton.onClick.AddListener(SpawnStar);
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

        instructionText.text = "Place another star";
        spawnButton.interactable = true;

        breathCounter++; // Increase number of breaths before showing pop up
        Debug.Log($"[Breath] Completed {breathCounter} breath(s)");

        if (breathCounter >= BreathsBeforePopUp)
        {
            Debug.Log("[Breath] Triggering popup via NotifyExhaleComplete()");
            BreathingManager.Instance?.NotifyExhaleComplete();
            breathCounter = 0;
        }
    }

    // Generate random position for the star
    Vector2 GetRandomPosition()
    {
        for (int attempts = 0; attempts < 50; attempts++)
        {
            // Get random position within the canvas
            float halfWidth = canvasTransform.rect.width / 2;
            float halfHeight = canvasTransform.rect.height / 2;

            // Calculate the star's width and height
            float starWidth = starPrefab.GetComponent<RectTransform>().sizeDelta.x;
            float starHeight = starPrefab.GetComponent<RectTransform>().sizeDelta.y;

            // Calculate the minimum and maximum X and Y positions for the star
            float minX = -halfWidth + (starWidth / 2);
            float maxX = halfWidth - (starWidth / 2);
            float minY = -halfHeight + (starHeight / 2);
            float maxY = halfHeight - (starHeight / 2);

            // Generate a random position within the calculated bounds
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 newPosition = new Vector2(randomX, randomY);

            // Check if the new position is too close to existing stars
            bool tooClose = false;
            foreach (Vector2 existingPosition in existingStars)
            {
                if (Vector2.Distance(newPosition, existingPosition) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }
            // If the new position is not too close to any existing stars, return it
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

        // If label changes, fade
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
