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
    }

    void Update()
    {
        // If the Colour Picker is active, update the instruction
        if (colourPicker != null && colourPicker.gameObject.activeSelf)
        {
            instructionText.text = "Choose a colour for your object";
        }
        else if (!objectPreviewLoaded && planeManager.trackables.count > 0)
        {
            instructionText.text = "Imagine your anxiety is a physical object. Place it in front of you";
            objectPreviewLoaded = true;
        }
    }
}
