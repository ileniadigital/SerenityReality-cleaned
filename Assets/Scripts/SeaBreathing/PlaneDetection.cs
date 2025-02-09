using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneDetection : MonoBehaviour
{
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private TextMeshProUGUI instructionText;
    private bool planeDetected = false;

    private void Start()
    {
        instructionText.text = "Scan the floor to detect a surface";
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDestroy()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (!planeDetected && args.added.Count > 0)
        {
            planeDetected = true;
            instructionText.text = "Imagine you are on the shore of a beach";
        }
    }
}
