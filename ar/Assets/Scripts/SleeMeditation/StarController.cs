using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    public GameObject starPrefab; // Drag the star prefab here
    public RectTransform canvasTransform; // Assign the Canvas
    public Button spawnButton; // Assign the UI button

    void Start()
    {
        spawnButton.onClick.AddListener(SpawnStar);
    }

    public void SpawnStar()
    {
        Debug.Log("🌟 Button Clicked!"); // Debugging log

        if (starPrefab == null)
        {
            Debug.LogError("🚨 No Star Prefab Assigned!");
            return;
        }

        if (canvasTransform == null)
        {
            Debug.LogError("🚨 No Canvas Assigned!");
            return;
        }

        // Generate a random position within the canvas
        float randomX = Random.Range(-canvasTransform.rect.width / 2, canvasTransform.rect.width / 2);
        float randomY = Random.Range(-canvasTransform.rect.height / 2, canvasTransform.rect.height / 2);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        Debug.Log("📍 New Star Position: " + randomPosition);

        // Instantiate star inside Canvas
        GameObject newStar = Instantiate(starPrefab, canvasTransform);
        newStar.GetComponent<RectTransform>().anchoredPosition = randomPosition;

        Debug.Log("✅ Star Spawned!");
    }

}
