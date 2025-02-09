using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BuzzControl : MonoBehaviour
{
    [SerializeField] private Slider buzzSpeedSlider;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private InstructionText instructionText;
    [SerializeField] private Button confirmButton;
    [SerializeField] public GameObject buzzUI;
    [SerializeField] private BreathingExercise breathingExercise;
    //[SerializeField] private PinchToResize pinchToResize;

    private Buzz buzzObject;
    private Buzz buzz;
    private bool isSecondBuzz = false;

    private Buzz Buzz; // This will be assigned dynamically

    private void Start()
    {
        buzzUI.SetActive(false);

        if (instructionText == null)
        {
            instructionText = FindObjectOfType<InstructionText>();
        }
        buzzSpeedSlider.onValueChanged.AddListener(UpdateBuzzSpeed);
        confirmButton.onClick.AddListener(ConfirmBuzz);
    }

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
        buzzUI.SetActive(false);

        if (!isSecondBuzz)
        {
            // First time confirming: Start breathing exercise
            if (instructionText != null)
            {
                instructionText.ShowBreathingInstruction();
            }

            StartCoroutine(StartBreathingExercise());
        }
        else
        {
            // Second time confirming: No more breathing instructions, just close UI
            if (instructionText != null)
            {
                instructionText.instructionText.text = "";
            }

            //// Enable pinching
            //pinchToResize.canPinchResize = true;
        }
    }


    public void ShowBuzzUI()
    {
        buzzUI.SetActive(true);
        if (instructionText != null)
        {
            if (!isSecondBuzz)
            {
                instructionText.instructionText.text = "How fast does your anxiety move?";
            }
            else
            {
                instructionText.instructionText.text = "Slow down your anxiety as you breathe.";
            }
        }
    }

    private IEnumerator StartBreathingExercise()
    {
        yield return new WaitForSeconds(1f);
        breathingExercise.StartBreathing();
        yield return new WaitForSeconds(67f);
        isSecondBuzz = true;
        ShowBuzzUI();
    }
}
