using UnityEngine;

public class BackButtonUI : MonoBehaviour
{
    public GameObject confirmationPanel;

    public void OnBackButtonPressed()
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
    {
        Debug.Log("Quitting app.");
        Application.Quit();
    }

    public void CancelExit()
    {
        Debug.Log("Cancel exit.");
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }
}
