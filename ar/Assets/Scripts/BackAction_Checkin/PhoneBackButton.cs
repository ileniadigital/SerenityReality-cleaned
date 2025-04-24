using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AndroidBackButtonHoldExit : MonoBehaviour
{
    public GameObject messagePanel;
    public float holdDuration = 1.2f; // how long they must hold to exit

    private bool menuVisible = false;
    private bool isHolding = false;
    private float holdStartTime = 0f;

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var escapeKey = Keyboard.current?.escapeKey;

        if (escapeKey == null)
            return;

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

        if (menuVisible && escapeKey.isPressed)
        {
            if (!isHolding)
            {
                isHolding = true;
                holdStartTime = Time.time;
            }
            else if (Time.time - holdStartTime >= holdDuration)
            {
                Debug.Log("Held long enough, exiting app.");
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
    {
        if (messagePanel != null)
            messagePanel.SetActive(true);

    }

    void HideMessage()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#endif
    }
}
