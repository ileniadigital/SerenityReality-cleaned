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

        // Inhale Phase (4 seconds) - Brighten
        for (int i = 4; i > 0; i--)
        {
            instructionText.text = "Inhale for\n " + i;
            float brightness = Mathf.Lerp(1f, 3f, 1 - (i / 4f)); // Overbrighten effect
            starImage.color = new Color(brightness, brightness, brightness, 1f);
            yield return new WaitForSeconds(1f);
        }

        // Hold Breath Phase (7 seconds) - Stay bright
        instructionText.text = "Hold for\n 7";
        starImage.color = new Color(3f, 3f, 3f, 1f); // Peak brightness
        yield return new WaitForSeconds(1f);
        for (int i = 6; i > 0; i--)
        {
            instructionText.text = "Hold for\n " + i;
            yield return new WaitForSeconds(1f);
        }

        // Exhale Phase (8 seconds) - Dim back
        for (int i = 8; i > 0; i--)
        {
            instructionText.text = "Exhale for\n " + i;
            float brightness = Mathf.Lerp(3f, 0.5f, 1 - (i / 8f)); // Gradually dim
            starImage.color = new Color(brightness, brightness, brightness, 1f);
            yield return new WaitForSeconds(1f);
        }

        instructionText.text = "Place another star";
        spawnButton.interactable = true;
    }


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

}