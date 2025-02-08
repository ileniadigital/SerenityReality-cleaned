using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuzzControl : MonoBehaviour
{
    [SerializeField] private Slider buzzSpeedSlider;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject buzzUI;
    [SerializeField] private BreathingExercise breathingExercise;

    private Buzz buzzObject;

    private Buzz Buzz; // This will be assigned dynamically

    private void Start()
    {
        buzzUI.SetActive(false);

        buzzSpeedSlider.onValueChanged.AddListener(UpdateBuzzSpeed);
        confirmButton.onClick.AddListener(ConfirmBuzz);
    }

    // Dynamically assign the object that should vibrate
    public void SetBuzzObject(GameObject newObject)
    {
        Buzz = newObject.GetComponent<Buzz>();
    }

    private void UpdateBuzzSpeed(float speed)
    {
        if (Buzz != null)
        {
            Buzz.SetBuzzSpeed(speed);
        }

        if (speedText != null)
        {
            speedText.text = $"Speed: {speed:F2}";
        }
    }

    private void ConfirmBuzz()
    {
        if (Buzz != null)
        {
            Buzz.StopBuzz();
        }

        buzzUI.SetActive(false);

        // Start breathing exercise
        breathingExercise.StartBreathing();
    }

    public void ShowBuzzUI()
    {
        buzzUI.SetActive(true);
    }
}
