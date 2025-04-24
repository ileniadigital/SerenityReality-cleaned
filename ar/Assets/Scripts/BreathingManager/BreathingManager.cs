using UnityEngine;

public class BreathingManager : MonoBehaviour
{
    public static BreathingManager Instance;
    private IBreathingController controller;

    public GameObject anxietyPopupPanel;
    public float checkInterval = 120f; // every 2 minutes

    private float timer = 0f;
    private bool isWaitingToPrompt = false;

    void Awake()
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
        if (isWaitingToPrompt)
        {
            ShowPrompt();
            isWaitingToPrompt = false;
            timer = 0f;
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
