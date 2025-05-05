// Used to pinch the element to resize it
// NOT IMPLEMENTED YET
using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class PinchToResize : MonoBehaviour
{
    public bool canPinchResize = false;
    private float initialPinchDistance;
    private Vector3 initialScale;
    private bool isPinching = false;

    private InputDevice leftController;
    private InputDevice rightController;

    [SerializeField] private InstructionText instructionText;
    [SerializeField] private float animationDuration = 5f;  // Duration of the shrinking animation

    private void Start()
    {
        TryInitializeXRControllers();

        // If instructionText is not assigned, try finding it
        if (instructionText == null)
        {
            instructionText = FindObjectOfType<InstructionText>();
        }
    }

    private void Update()
    {
        if (!canPinchResize) return;  // Only allow pinch-to-resize if enabled

        DetectPinchStartAndEnd();
    }

    // Initialize the XR controllers
    private void TryInitializeXRControllers()
    {
        // Fetch the left and right controllers from XR input system
        var leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        var rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (leftHand.isValid && rightHand.isValid)
        {
            leftController = leftHand;
            rightController = rightHand;
        }
    }

    // Detect when a pinch starts and ends
    private void DetectPinchStartAndEnd()
    {
        if (leftController.isValid && rightController.isValid)
        {
            bool isLeftPinching = leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftPinch) && leftPinch;
            bool isRightPinching = rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightPinch) && rightPinch;

            // Pinch gesture starts (both controllers trigger the pinch)
            if (isLeftPinching && isRightPinching && !isPinching)
            {
                isPinching = true;
                initialPinchDistance = GetControllersDistance();
                initialScale = transform.localScale;

                // Update instruction text when pinch starts
                if (instructionText != null)
                {
                    instructionText.SetInstructionText("Pinch your anxiety until it disappears");
                }

                // Start the shrinking animation
                StartCoroutine(ShrinkAnimation());
            }
            // Pinch gesture ends (either controller releases the pinch)
            else if ((!isLeftPinching || !isRightPinching) && isPinching)
            {
                isPinching = false;

                // Update instruction text when pinch ends
                if (instructionText != null)
                {
                    instructionText.SetInstructionText("Let the weight of anxiety go as you breathe");
                }
            }
        }
    }

    // Get the distance between both controllers
    private float GetControllersDistance()
    {
        Vector3 leftPos, rightPos;
        if (leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftPos) &&
            rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightPos))
        {
            return Vector3.Distance(leftPos, rightPos);
        }
        return 0f;
    }

    // Coroutine to animate the shrinking of the object
    private IEnumerator ShrinkAnimation()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        // Shrink the object over time
        while (elapsedTime < animationDuration)
        {
            float lerpFactor = elapsedTime / animationDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, lerpFactor);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly zero
        transform.localScale = Vector3.zero;

        // Optionally hide the object or disable it when it shrinks to zero
        transform.gameObject.SetActive(false);
    }
}
