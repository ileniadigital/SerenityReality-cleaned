// Handles the back button on Android devices
// Shows a message panel when the back button is pressed and allows the user to exit the app by holding the button
//
// This script must be attached to a GameObject, with the messagePanel object assigned in the Unity Inspector
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AndroidBackButtonHoldExit : MonoBehaviour
{
    public GameObject messagePanel; // messagePanel to show when the back button is pressed
    public float holdDuration = 1.2f; // how long they must hold to exit

    private bool menuVisible = false; // whether the message panel is currently visible
    private bool isHolding = false; // whether the back button is being held down
    private float holdStartTime = 0f; // time when the back button was first pressed

    void Update()
    // Check if the back button is being pressed
    {
        // Android back button handling
#if UNITY_ANDROID && !UNITY_EDITOR
        var escapeKey = Keyboard.current?.escapeKey;

        // Check if the escape key is null 
        if (escapeKey == null)
            return;

        // Check if the escape key was pressed
        if (escapeKey.wasPressedThisFrame)
        {
            if (!menuVisible)
            {
                ShowMessage("Press and hold to exit");
                Vibrate();
                menuVisible = true;
            }
            else
            {
                HideMessage();
                menuVisible = false;
            }
        }

        // Check if the escape key is being pressed
        if (menuVisible && escapeKey.isPressed)
        {
            if (!isHolding)
            {
                isHolding = true;
                holdStartTime = Time.time;
            }
            else if (Time.time - holdStartTime >= holdDuration)
            {
                Debug.Log("Exit button held, exiting app.");
                Application.Quit();
            }
        }

        if (escapeKey.wasReleasedThisFrame)
        {
            isHolding = false;
        }
#endif
    }

    void ShowMessage(string msg)
    // Show the message panel
    {
        if (messagePanel != null)
            messagePanel.SetActive(true);

    }

    void HideMessage()
    // Hide the message panel
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    void Vibrate()
    // Vibrate the phone if on Android and back button is being pressed
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#endif
    }
}
