using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneBackButton : MonoBehaviour
{
    public GameObject confirmationPanel;

    private bool isShowingConfirmation = false;

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            HandleBackButton();
        }
#endif
    }

    private void HandleBackButton()
    {
        Debug.Log("Back button pressed.");

        if (confirmationPanel == null)
        {
            Debug.LogWarning("Confirmation panel not assigned.");
            return;
        }

        if (!isShowingConfirmation)
        {
            confirmationPanel.SetActive(true);
            isShowingConfirmation = true;
        }
        else
        {
            confirmationPanel.SetActive(false);
            isShowingConfirmation = false;
        }
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        confirmationPanel.SetActive(false);
        isShowingConfirmation = false;
    }
}
