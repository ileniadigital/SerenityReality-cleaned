using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/*
 * BuzzControl class handles buzzing animation of the object
 */
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
    private bool isSecondBuzz = false;
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
        buzzObject = newObject.GetComponent<Buzz>();
    }

    private void UpdateBuzzSpeed(float speed)
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
