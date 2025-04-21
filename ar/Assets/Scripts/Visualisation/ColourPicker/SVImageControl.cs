using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image picker;
    private RawImage sImage;
    private ColourPickerControl cc;
    private RectTransform rectTransform, pickerTransform;

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    private void Awake()
    {
        sImage = GetComponent<RawImage>();
        cc = FindObjectOfType<ColourPickerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = picker.GetComponent<RectTransform>();
        pickerTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    void UpdateColour(PointerEventData eventData)
    {
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);
        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        if (pos.x < -deltaX) {
            pos.x = -deltaX;
        } else if (pos.x > deltaX) {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY)
        {
            pos.y = -deltaY;
        }
        else if (pos.y > deltaY) {
            pos.y = deltaY;
        }

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        // Normalise positions
        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        // Update picker position
        pickerTransform.localPosition = pos;
        picker.color = Color.HSVToRGB(0, 0, 1 - yNorm); // Makes the picker the opposite colour of the picked colour

        cc.SetSV(xNorm, yNorm);
    }
}
