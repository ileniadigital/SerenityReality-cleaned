// BoldbuttonOnClick makes the text of a butotn bold on-click
//
// This script must be attached to a button with a TMP_Text component as a child.
using UnityEngine;
using TMPro;

public class BoldButtonOnClick : MonoBehaviour
{
    [SerializeField] private TMP_Text text; // The text component to be bolded
    [SerializeField] private float boldDuration = 2f; // Duration for which the text will be bolded

    private FontWeight originalWeight; // The original font weight of the text

    void Start()
    // Initialize the original font weight of the text component
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        originalWeight = text.fontWeight;
    }

    public void BoldOnClick()
    // Make text bold when the button is clicked
    {
        if (text == null) return;

        StopAllCoroutines();
        StartCoroutine(BoldEffect());
    }

    private System.Collections.IEnumerator BoldEffect()
    // Coroutine to handle the bold effect
    {
        text.fontWeight = FontWeight.Bold;
        yield return new WaitForSeconds(boldDuration);
        text.fontWeight = originalWeight;
    }
}
