using UnityEngine;

public class ARExitGesture : MonoBehaviour
{
    public GameObject confirmationPanel;

    private float lastTapTime;
    private float tapCooldown = 1.0f;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                // Debounce: prevent multiple triggers
                if (Time.time - lastTapTime > tapCooldown)
                {
                    Debug.Log("Two-finger tap detected");
                    lastTapTime = Time.time;

                    if (confirmationPanel != null)
                        confirmationPanel.SetActive(true);
                    else
                        Application.Quit(); // fallback
                }
            }
        }
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);
    }
}
