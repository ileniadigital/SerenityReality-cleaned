using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class InstructionText : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    private ARPlaneManager planeManager;
    private bool objectPreviewLoaded = false;
    [SerializeField] private ColourPickerControl colourPicker;
    [SerializeField] private BuzzControl buzzControl;

    void Start()
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
    {
        instructionText.text = "Take 3 deep breaths";
    }

    public void HideBreathingInstruction()
    {
        instructionText.text = "";
    }
}
