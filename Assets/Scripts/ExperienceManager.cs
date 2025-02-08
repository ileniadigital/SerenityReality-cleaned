using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private Button addButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject shapeMenu;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private ARPlaneManager aRPlaneManager;
    [SerializeField] private GameObject[] objectPrefab;
    [SerializeField] private ColourPickerControl colourPicker;

    public GameObject createdObject;
    private GameObject _currentPrefab;
    private bool _canAddObject = false;
    private bool _shapeConfirmed = false;
    private GameObject _objectPrefabPreview;
    private Vector3 _detectedPosition = new Vector3();
    private Quaternion _detectedRotation = Quaternion.identity;
    private ARTrackable _currentTrackable = null;

    private void Start()
    {
        // Hide all elements initially
        colourPicker.ToggleColourPicker(false);
        addButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
        shapeMenu.gameObject.SetActive(true);

        // Attach the SpawnObject function to the Add button's onClick event
        if (addButton != null)
        {
            addButton.onClick.AddListener(SpawnObject);
        }

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(ConfirmShapeSelection);
        }
    }


    private void Update()
    {
        // Update the detected position and rotation for the preview
        GetRaycastHitTransform();
    }

    private void GetRaycastHitTransform()
    {
        var hits = new List<ARRaycastHit>();
        var middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        // Perform a raycast to detect AR planes
        if (aRRaycastManager.Raycast(middleScreen, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            _detectedPosition = hits[0].pose.position;
            _detectedRotation = hits[0].pose.rotation;
            _currentTrackable = hits[0].trackable;

            if (_objectPrefabPreview != null)
            {
                _objectPrefabPreview.transform.position = _detectedPosition;
                _objectPrefabPreview.transform.rotation = _detectedRotation;
            }
        }
    }

    public void ChangeSelectedObject(int index)
    {
        if (index < 0 || index >= objectPrefab.Length) return;

        _currentPrefab = objectPrefab[index];

        if (_objectPrefabPreview != null)
        {
            Destroy(_objectPrefabPreview);
        }

        _objectPrefabPreview = Instantiate(_currentPrefab);
        _objectPrefabPreview.SetActive(true);
        _objectPrefabPreview.transform.position = _detectedPosition;
        _objectPrefabPreview.transform.rotation = _detectedRotation;

        confirmButton.gameObject.SetActive(true);
    }

    public void ConfirmShapeSelection()
    {
        if (_currentPrefab == null) return;

        _shapeConfirmed = true;

        shapeMenu.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);

        addButton.gameObject.SetActive(true);
        addButton.interactable = true;

    }

    public void SpawnObject()
    {
        if (!_shapeConfirmed || _currentPrefab == null) return;

        // Instantiate a new object
        var objectCreated = Instantiate(_currentPrefab);
        objectCreated.transform.position = _detectedPosition;
        objectCreated.transform.rotation = _detectedRotation;

        // Attach to the detected plane
        if (_currentTrackable != null)
        {
            objectCreated.GetComponent<Object>().PlaceObject(_currentTrackable);
        }

        addButton.gameObject.SetActive(false);
        addButton.interactable = false;

        if (_objectPrefabPreview != null)
        {
            _objectPrefabPreview.SetActive(false);
        }

        // Disable plane visualization
        DisablePlaneVisualisation();

        createdObject = objectCreated;
        // Show colour picker
        colourPicker.ToggleColourPicker(true);
        // Notify VibrationControl about the new spawned object
        FindObjectOfType<BuzzControl>().SetBuzzObject(objectCreated);

    }

    private void OnDestroy()
    {
        if (_canAddObject)
        {
            InputHandler.OnTap -= SpawnObject;
        }
    }

    public void SetCanAddObject(bool canAddObject)
    {
        _canAddObject = canAddObject;
    }

    private void DisablePlaneVisualisation()
    {
        aRPlaneManager.enabled = false;
        foreach (var plane in aRPlaneManager.trackables) {
            var planeVisualiser = plane.GetComponent<ARFeatheredPlaneMeshVisualizerCompanion>();
            if (planeVisualiser != null) {
                planeVisualiser.visualizeSurfaces = false;
            }
        }
    }
}