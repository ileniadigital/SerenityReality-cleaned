// InstructionText handles the animation and changes in text of the Visualisation scene
//
// It must be attached to the GameObject that contains the TextMeshProUGUI component
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class InstructionText : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the TextMeshProUGUI component
    private ARPlaneManager planeManager; // Reference to the ARPlaneManager component
    private bool objectPreviewLoaded = false; // Flag to check if the object preview has been loaded
    [SerializeField] private ColourPickerControl colourPicker; // Reference to the ColourPickerControl component
    [SerializeField] private BuzzControl buzzControl; // Reference to the BuzzControl component

    void Start()
    // Set the initial text and initialise all components
    {
        instructionText.text = "Scan the room to detect available surfaces";
        planeManager = FindObjectOfType<ARPlaneManager>();

        if (planeManager == null)
        {
            Debug.LogError("ARPlaneManager not found");
        }

        if (colourPicker == null)
        {
            colourPicker = FindObjectOfType<ColourPickerControl>();
        }
        if (buzzControl == null)
        {
            buzzControl = FindObjectOfType<BuzzControl>();
        }
    }

    void Update()
    {
        // If the Buzz menu is active, update instruction text
        //if (buzzControl != null && buzzControl.buzzUI.activeSelf)
        //{
        //    instructionText.text = "How fast does your anxiety move?";
        //}
        // If the Colour Picker is active, update the instruction
        if (colourPicker != null && colourPicker.gameObject.activeSelf)
        {
            instructionText.text = "What colour is your anxiety?";
        }
        else if (!objectPreviewLoaded && planeManager.trackables.count > 0)
        {
            instructionText.text = "Imagine your anxiety is a physical object. Place it in front of you";
            objectPreviewLoaded = true;
        }

    }

    public void ShowBreathingInstruction()
    // Show the breathing instruction text
    {
        instructionText.text = "Take 3 deep breaths";
    }

    public void SetInstructionText(string text)
    // Set the instruction text to the specified string
    {
        instructionText.text = text;
    }
}
