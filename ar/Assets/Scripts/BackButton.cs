using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject confirmationPanel;

    private bool isConfirming = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isConfirming)
            {
                confirmationPanel.SetActive(true);
                isConfirming = true;
            }
            else
            {
                // If back is pressed again while confirming, just close the panel
                confirmationPanel.SetActive(false);
                isConfirming = false;
            }
        }
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        confirmationPanel.SetActive(false);
        isConfirming = false;
    }
}
