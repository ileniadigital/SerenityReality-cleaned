/// The BackButtonUI handles the behaviour of a back button in the scenes.
/// It provides functionality to display the confirmation panel when the back button is pressed.
/// 
/// This script must bee attached to a GameObject, with the confirmationPanel object assigned in the Unity Inspector.
/// Ensure that the <c>confirmationPanel</c> GameObject is assigned in the Unity Inspector
using UnityEngine;

public class BackButtonUI : MonoBehaviour
{
    public GameObject confirmationPanel; // confirmationPanel object to be displayed

    public void OnBackButtonPressed()
    // Set the confirmationPanel to active when the back button is pressed
    {
        Debug.Log("Back button clicked.");
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Confirmation Panel not assigned.");
        }
    }

    public void ConfirmExit()
    // Quit the application when the user confirms the exit action
    {
        Debug.Log("Quitting app.");
        Application.Quit();
    }

    public void CancelExit()
    // Hide the confirmationPanel when the user cancels the exit action
    {
        Debug.Log("Cancel exit.");
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }
}
