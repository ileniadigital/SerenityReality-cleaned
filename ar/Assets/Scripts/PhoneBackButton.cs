using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PhoneBackButton : MonoBehaviour
{
    public GameObject messagePanel;
    public float exitTimeout = 2f;         // Time allowed for second press

    private bool isWaitingForSecondPress = false;
    private float lastBackPressTime;

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            HandleBackPress();
        }
#endif
    }

    void HandleBackPress()
    {
        float timeSinceLastPress = Time.time - lastBackPressTime;

        if (!isWaitingForSecondPress || timeSinceLastPress > exitTimeout)
        {
            // First press or timeout passed
            isWaitingForSecondPress = true;
            lastBackPressTime = Time.time;

            if (messagePanel != null)
                messagePanel.SetActive(true);
        }
        else
        {
            // Second press within timeout
            Debug.Log("Exiting app via double back press.");
            Application.Quit();
        }
    }

    void LateUpdate()
    {
        // Hide the message if timeout passed
        if (isWaitingForSecondPress && (Time.time - lastBackPressTime > exitTimeout))
        {
            isWaitingForSecondPress = false;
            if (messagePanel != null)
                messagePanel.SetActive(false);
        }
    }
}
