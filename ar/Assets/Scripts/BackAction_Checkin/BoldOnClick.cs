using UnityEngine;
using TMPro;

public class BoldButtonOnClick : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float boldDuration = 2f;

    private FontWeight originalWeight;

    void Start()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        originalWeight = text.fontWeight;
    }

    public void BoldOnClick()
    {
        if (text == null) return;

        StopAllCoroutines();
        StartCoroutine(BoldEffect());
    }

    private System.Collections.IEnumerator BoldEffect()
    {
        text.fontWeight = FontWeight.Bold;
        yield return new WaitForSeconds(boldDuration);
        text.fontWeight = originalWeight;
    }
}
