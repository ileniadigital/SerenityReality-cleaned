// BreathingManager handles the breathing exercise and prompts the user to rate their anxiety level after a set interval.
//
// It must assigned to a GameObject in the Unity scene, and the anxietyPopupPanel object must be assigned in the Unity Inspector.
using UnityEngine;

public class BreathingManager : MonoBehaviour
{
    public static BreathingManager Instance; // Singleton instance
    private IBreathingController controller; // Breathing controller

    public GameObject anxietyPopupPanel; // Anxiety panel to be assigned in the Inspector
    public float checkInterval = 120f; // every 2 minutes

    private float timer = 0f; // Timer to track the interval
    private bool isWaitingToPrompt = false; // Flag to check if we are waiting to prompt the user

    void Awake()
    // Singleton pattern to ensure only one instance of BreathingManager exists
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Find Breathing contoller to pause breathing when showing prompt
    void FindController()
    {
        controller = FindObjectOfType<MonoBehaviour>() as IBreathingController;
    }

    void Update()
    // Update timer each frame and check if we need to prompt the user
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval && !isWaitingToPrompt)
        {
            isWaitingToPrompt = true;
        }
    }

    // Called by breathing script after exhale completes
    public void NotifyExhaleComplete()
    {
        Debug.Log("[Popup] NotifyExhaleComplete called");
        if (isWaitingToPrompt)
        {
            ShowPrompt();
            isWaitingToPrompt = false;
            timer = 0f;
        }
        else
        {
            Debug.LogWarning("[Popup] Panel is null!");
        }
    }

    // Show anxiety rating pop up
    void ShowPrompt()
    {
        Debug.Log("Prompting user to rate anxiety");
        if (anxietyPopupPanel != null)
        {
            anxietyPopupPanel.SetActive(true);
            FindController();
            controller?.PauseBreathing();
        }
    }

    // Close anxiety rating prompt if user dismisses it
    public void DismissPrompt()
    {
        if (anxietyPopupPanel != null)
        {
            anxietyPopupPanel.SetActive(false);
            controller?.ResumeBreathing();
        }
    }
}
