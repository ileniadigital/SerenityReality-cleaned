// Handles the colour picker functionaility
//
// It must be attached to the ColourPicker prefab in the scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourPickerControl : MonoBehaviour
{
    public float currentH, currentS, currentV; // Current HSV values
    [SerializeField] private RawImage hImage, sImage, previewImage; // RawImage components for hue, saturation, and preview images
    [SerializeField] private Slider slider; // Slider for hue selection
    [SerializeField] private Button confirmButton; // Button to confirm the selected colour

    private Texture2D hTexture, sTexture, previewTexture; // Textures for hue, saturation, and preview images

    [SerializeField] MeshRenderer changeColour; // MeshRenderer to change the colour of the object

    private ExperienceManager experienceManager; // Reference to the ExperienceManager script
    private MeshRenderer targetRenderer; // MeshRenderer of the target object to change colour

    private void Start()
    // Initialise the panel
    {
        //ToggleColourPicker(false);
        experienceManager = FindObjectOfType<ExperienceManager>();
        CreateHueImage();
        CreateSatImage();
        CreatePreviewImage();

        UpdatePreview();

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(ConfirmColour);
        }
    }

    private void CreateHueImage()
    // Create the hue image texture
    {
        hTexture = new Texture2D(1, 16);
        hTexture.wrapMode = TextureWrapMode.Clamp;
        hTexture.name = "Hue Texture";

        for (int i = 0; i < hTexture.height; i++)
        {
            hTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hTexture.height, 1, 0.05f));
        }

        hTexture.Apply();
        currentH = 0;

        hImage.texture = hTexture;
    }

    private void CreateSatImage()
    // Create the saturation image texture
    {
        sTexture = new Texture2D(16, 16);
        sTexture.wrapMode = TextureWrapMode.Clamp;
        sTexture.name = "Sat Texture";

        for (int y = 0; y < sTexture.height; y++)
        {
            for (int x = 0; x < sTexture.width; x++)
            {
                sTexture.SetPixel(x, y, Color.HSVToRGB(currentH, (float)x / sTexture.width, (float)y / sTexture.height));
            }
        }

        sTexture.Apply();
        currentS = 0;
        currentV = 0;

        sImage.texture = sTexture;
    }

    private void CreatePreviewImage()
    // Create the preview image texture
    {
        previewTexture = new Texture2D(1, 16);
        previewTexture.wrapMode = TextureWrapMode.Clamp;
        previewTexture.name = "Preview Texture";

        Color currentColour = Color.HSVToRGB(currentH, currentS, currentV);

        for (int i = 0; i < previewTexture.height; i++)
        {
            previewTexture.SetPixel(0, i, currentColour);
        }

        previewTexture.Apply();
        previewImage.texture = previewTexture;
    }

    private void UpdatePreview()
    // Update the preview image with the current HSV values
    {
        Color currentColour = Color.HSVToRGB(currentH, currentS, currentV);
        for (int i = 0; i < previewTexture.height; i++)
        {
            previewTexture.SetPixel(0, i, currentColour);
        }

        previewTexture.Apply();
        //changeColour.material.SetColor("_BaseColor", currentColour);
        //changeColour.GetComponent<MeshRenderer>().material.color = currentColour;
        if (experienceManager != null && experienceManager.createdObject != null)
        {
            targetRenderer = experienceManager.createdObject.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = currentColour;
            }
        }
    }

    public void SetSV(float s, float v)
    // Set the saturation and value based on the slider position
    {
        currentS = s;
        currentV = v;
        UpdatePreview();
    }

    public void UpdateSV()
    // Update saturation value based on selection
    {
        currentH = slider.value;
        for (int y = 0; y < sTexture.height; y++)
        {
            for (int x = 0; x < sTexture.width; x++)
            {
                sTexture.SetPixel(x, y, Color.HSVToRGB(currentH, (float)x / sTexture.width, (float)y / sTexture.height));
            }
        }
        sTexture.Apply();
        UpdatePreview();
    }

    public void ToggleColourPicker(bool show)
    // Initialise colour picker UI
    {
        gameObject.SetActive(show);
    }

    private void ConfirmColour()
    // Confirm selected colour and apply it to the object
    {
        if (experienceManager != null && experienceManager.createdObject != null)
        {
            targetRenderer = experienceManager.createdObject.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                //Material newMaterial = new Material(targetRenderer.material);
                //targetRenderer.material = newMaterial;
                // Apply the final color permanently
                targetRenderer.material.color = Color.HSVToRGB(currentH, currentS, currentV);
            }
        }

        // Hide the colour picker UI
        ToggleColourPicker(false);

        // Show vibration UI after selecting color
        FindObjectOfType<BuzzControl>().ShowBuzzUI();
    }
}
