using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public RectTransform canvasTransform;
    public Button spawnButton;
    public TextMeshProUGUI instructionText;

    private List<Vector2> existingStars = new List<Vector2>(); // Store placed stars
    private float minDistance = 100f; // Minimum distance between stars

    void Start()
    {
        spawnButton.onClick.RemoveAllListeners();
        spawnButton.onClick.AddListener(SpawnStar);
    }

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

    IEnumerator StarGlowSequence(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();

        // Countdown for inhale (5 → 4 → 3 → 2 → 1)
        for (int i = 5; i > 0; i--)
        {
            instructionText.text = "Inhale for " + i;
            yield return new WaitForSeconds(1f);
        }

        // Brighten the star over 4 seconds
        for (float t = 0; t < 4f; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0.5f, 1f, t / 4f);
            starImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        // Hold brightness (7 seconds)
        instructionText.text = "Hold your breath...";
        yield return new WaitForSeconds(7f);

        // Countdown for exhale (8 → 7 → 6 → ... → 1)
        for (int i = 8; i > 0; i--)
        {
            instructionText.text = "Exhale for " + i;
            float alpha = Mathf.Lerp(1f, 0.5f, 1f - (i / 8f));
            starImage.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(1f);
        }

        instructionText.text = "Place another star";
        spawnButton.interactable = true;
    }

    Vector2 GetRandomPosition()
    {
        for (int attempts = 0; attempts < 50; attempts++)
        {
            float randomX = Random.Range(-canvasTransform.rect.width / 2, canvasTransform.rect.width / 2);
            float randomY = Random.Range(-canvasTransform.rect.height / 2, canvasTransform.rect.height / 2);
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

        return Vector2.zero; // Return (0,0) if no space found
    }
}
