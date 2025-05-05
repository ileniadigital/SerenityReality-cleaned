// Manages the AR experiences, including object placement and interaction
//
// It must be attached to the ExperienceManager prefab in the scene
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private Button addButton; // Button to add the object
    [SerializeField] private Button confirmButton; // Button to confirm the shape selection
    [SerializeField] private GameObject shapeMenu; // Menu for selecting the shape
    [SerializeField] private ARRaycastManager aRRaycastManager; // Raycast manager for detecting AR planes
    [SerializeField] private ARPlaneManager aRPlaneManager; // Plane manager for managing AR planes
    [SerializeField] private GameObject[] objectPrefab; // Array of object prefabs to choose from
    [SerializeField] private ColourPickerControl colourPicker; // Colour picker for selecting the object's colour

    public GameObject createdObject; // The created object
    private GameObject _currentPrefab; // The currently selected prefab
    private bool _canAddObject = false; // Flag to check if the object can be added
    private bool _shapeConfirmed = false; // Flag to check if the shape is confirmed
    private GameObject _objectPrefabPreview; // Preview of the object prefab
    private Vector3 _detectedPosition = new Vector3(); // Detected position for the object placement
    private Quaternion _detectedRotation = Quaternion.identity; // Detected rotation for the object placement
    private ARTrackable _currentTrackable = null; // Current trackable object

    private void Start()
    {
        // Hide all elements initially
        colourPicker.ToggleColourPicker(false);
        addButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        shapeMenu.gameObject.SetActive(true);

        StartCoroutine(ShowShapeMenuWithDelay(3f));

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
    // Perform a raycast to detect AR planes and update the position and rotation of the object preview
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

    private IEnumerator ShowShapeMenuWithDelay(float delay)
    // Show the shape menu after a delay
    {
        yield return new WaitForSeconds(delay);
        confirmButton.gameObject.SetActive(true);
    }

    public void ChangeSelectedObject(int index)
    // Change the selected object based on the index
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
    // Confirm the shape selection and enable the add button
    {
        if (_currentPrefab == null) return;

        _shapeConfirmed = true;

        shapeMenu.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);

        addButton.gameObject.SetActive(true);
        addButton.interactable = true;

    }

    public void SpawnObject()
    // Spawn the object at the detected position and rotation
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
    // Unsubscribe from event when object is destroyed
    {
        if (_canAddObject)
        {
            InputHandler.OnTap -= SpawnObject;
        }
    }

    public void SetCanAddObject(bool canAddObject)
    // Set the flag to check if the object can be added
    {
        _canAddObject = canAddObject;
    }

    private void DisablePlaneVisualisation()
    // Disable plane visualisation after the object is placed
    {
        aRPlaneManager.enabled = false;
        foreach (var plane in aRPlaneManager.trackables)
        {
            var planeVisualiser = plane.GetComponent<ARFeatheredPlaneMeshVisualizerCompanion>();
            if (planeVisualiser != null)
            {
                planeVisualiser.visualizeSurfaces = false;
            }
        }
    }
}