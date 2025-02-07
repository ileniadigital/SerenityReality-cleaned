using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourPickerControl : MonoBehaviour
{
    public float currentH, currentS, currentV;
    [SerializeField] private RawImage hImage, sImage, previewImage;
    [SerializeField] private Slider slider;
    [SerializeField] private Button confirmButton;

    private Texture2D hTexture, sTexture, previewTexture;

    [SerializeField] MeshRenderer changeColour;

    private ExperienceManager experienceManager;
    private MeshRenderer targetRenderer;

    private void Start()
    {
        //ToggleColourPicker(false);
        experienceManager = FindObjectOfType<ExperienceManager>();
        CreateHueImage();
        CreateSatImage();
        CreatePreviewImage();

        UpdatePreview();

        if (confirmButton != null) {
            confirmButton.onClick.AddListener(ConfirmColour);
        }
    }

    private void CreateHueImage()
    {
        hTexture = new Texture2D(1, 16);
        hTexture.wrapMode = TextureWrapMode.Clamp;
        hTexture.name = "Hue Texture";

        for (int i = 0; i < hTexture.height; i++) {
            hTexture.SetPixel(0, i, Color.HSVToRGB( (float) i / hTexture.height, 1, 0.05f));
        }

        hTexture.Apply();
        currentH = 0;

        hImage.texture = hTexture;
    }

    private void CreateSatImage()
    {
        sTexture = new Texture2D(16, 16);
        sTexture.wrapMode = TextureWrapMode.Clamp;
        sTexture.name = "Sat Texture";

        for (int y = 0; y < sTexture.height; y++)
        {
            for (int x = 0; x < sTexture.width; x++) {
                sTexture.SetPixel(x, y, Color.HSVToRGB(currentH, (float)x / sTexture.width, (float)y / sTexture.height));
            }
        }

        sTexture.Apply();
        currentS = 0;
        currentV = 0;

        sImage.texture = sTexture;
    }

    private void CreatePreviewImage()
    {
        previewTexture = new Texture2D(1, 16);
        previewTexture.wrapMode = TextureWrapMode.Clamp;
        previewTexture.name = "Preview Texture";

        Color currentColour = Color.HSVToRGB(currentH, currentS, currentV);

        for (int i = 0; i < previewTexture.height; i++) {
            previewTexture.SetPixel(0, i, currentColour);
        }

        previewTexture.Apply();
        previewImage.texture = previewTexture;
    }

    private void UpdatePreview()
    {
        Color currentColour = Color.HSVToRGB(currentH, currentS, currentV);
        for (int i = 0; i < previewTexture.height; i++) {
            previewTexture.SetPixel(0, i, currentColour);
        }

        previewTexture.Apply();
        //changeColour.material.SetColor("_BaseColor", currentColour);
        //changeColour.GetComponent<MeshRenderer>().material.color = currentColour;
        if (experienceManager != null && experienceManager.createdObject!= null)
        {
            targetRenderer = experienceManager.createdObject.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = currentColour;
            }
        }
    }

    public void SetSV(float s, float v)
    {
        currentS = s;
        currentV = v;
        UpdatePreview();
    }

    public void UpdateSV()
    {
        currentH = slider.value;
        for (int y = 0; y < sTexture.height; y++) {
            for (int x = 0; x < sTexture.width; x++) {
                sTexture.SetPixel(x, y, Color.HSVToRGB(currentH, (float)x / sTexture.width, (float) y / sTexture.height));
            }
        }
        sTexture.Apply();
        UpdatePreview();
    }

    public void ToggleColourPicker(bool show)
    {
        gameObject.SetActive(show);
    }

    private void ConfirmColour()
    {
        if (experienceManager != null && experienceManager.createdObject != null)
        {
            targetRenderer = experienceManager.createdObject.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                // Apply the final color permanently
                targetRenderer.material.color = Color.HSVToRGB(currentH, currentS, currentV);
            }
        }

        // Hide the colour picker UI
        ToggleColourPicker(false);
    }
}
