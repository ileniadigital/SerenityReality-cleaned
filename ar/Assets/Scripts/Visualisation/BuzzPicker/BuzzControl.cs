//  BuzzControl class handles buzzing animation of the object
//
// It must be attached to the object that controls the buzz speed

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BuzzControl : MonoBehaviour
{
    [SerializeField] private Slider buzzSpeedSlider; // Slider to control the speed of the buzz
    [SerializeField] private TextMeshProUGUI speedText; // Text to display the current speed of the buzz
    [SerializeField] private InstructionText instructionText; // Instruction text to guide the user
    [SerializeField] private Button confirmButton; // Button to confirm the buzz speed
    [SerializeField] public GameObject buzzUI; // UI element to show the buzz speed control
    [SerializeField] private BreathingExercise breathingExercise; // Breathing exercise object to be activated during the buzz
    //[SerializeField] private PinchToResize pinchToResize;

    private Buzz buzzObject;
    private bool isSecondBuzz = false;
    private void Start()
    // Initialise the buzz control and set up the control panel
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
    // Set the object to buzz and initialise the buzz component
    {
        buzzObject = newObject.GetComponent<Buzz>();
    }

    private void UpdateBuzzSpeed(float speed)
    // Update the speed of the buzz based on the slider value
    {
        if (buzzObject != null)
        {
            buzzObject.SetBuzzSpeed(speed);
        }

        if (speedText != null)
        {
            speedText.text = $"Speed: {speed:F2}";
        }
    }

    private void ConfirmBuzz()
    // Confirm the buzz speed and start breathing exercise
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
    // Show the UI panel to control the buzzing speed
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

    // Slow down the buzzing speed of the object during breathing
    private IEnumerator SlowDownBuzz(float duration)
    {
        float elapsed = 0f;
        float initialSpeed = buzzObject.GetBuzzSpeed(); // Get initial speed
        float targetSpeed = 0f; // Minimal speed

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Calculate rate of slowing down
            float newSpeed = Mathf.Lerp(initialSpeed, targetSpeed, t);
            buzzObject.SetBuzzSpeed(newSpeed); // Set new speed
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Stop buzzing
        buzzObject.SetBuzzSpeed(targetSpeed);
    }

    // Follow the breathing exercise and slow down object
    private IEnumerator StartBreathingExercise()
    {
        yield return new WaitForSeconds(1f); // wait 1 second

        // Start breathing exercise
        breathingExercise.gameObject.SetActive(true);
        breathingExercise.StartBreathing();

        // Slow down buzzing
        if (buzzObject != null)
            StartCoroutine(SlowDownBuzz(35f));

        yield return new WaitForSeconds(50f); // Duration of breathing exercise + 10

        // Set congratulatory text
        if (instructionText != null)
        {
            instructionText.instructionText.text = "Well done for visualising your anxiety.";
        }

        // Wait 5 seconds then quit
        yield return new WaitForSeconds(5f);

        Application.Quit();
    }



}
