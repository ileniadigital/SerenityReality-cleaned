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

    // Slow down the buzzing speed of the object during breathing
    private IEnumerator SlowDownBuzz(float duration)
    {
        float elapsed = 0f;
        float initialSpeed = Buzz.GetBuzzSpeed();
        float targetSpeed = 0.05f; // Minimal speed (or zero if you prefer)

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newSpeed = Mathf.Lerp(initialSpeed, targetSpeed, t);
            Buzz.SetBuzzSpeed(newSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final slowing down
        Buzz.SetBuzzSpeed(targetSpeed);
    }

    // Follow the breathing exercise and slow down object
    private IEnumerator StartBreathingExercise()
    {
        yield return new WaitForSeconds(1f);

        breathingExercise.gameObject.SetActive(true);
        breathingExercise.StartBreathing();

        if (Buzz != null)
            StartCoroutine(SlowDownBuzz(30f));

        yield return new WaitForSeconds(67f); // Duration of breathing exercise

        breathingExercise.StopBreathing();

        if (instructionText != null)
        {
            instructionText.instructionText.text = "Well done for visualising your anxiety.";
        }

        yield return new WaitForSeconds(5f);

        Application.Quit();
    }



}
