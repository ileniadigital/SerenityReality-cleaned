// Selects the surface based on the scanned AR world
//
// It must be attached to the AR world in the AR scene
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class SurfaceSelection : MonoBehaviour
{
    public ARRaycastManager raycastManager; // The AR Raycast Manager to detect planes
    public GameObject placementIndicator; // The visual indicator for placement

    public GameObject textPrefab; // The prefab for the text to be placed
    public GameObject objectPrefab; // The prefab for the object to be placed
    private GameObject spawnedText; // The spawned text object
    private Pose placementPose; // The pose for the placement indicator
    private bool placementPoseIsValid = false; // Flag to check if the placement pose is valid

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceText();
            PlaceObject();
        }
    }

    private void UpdatePlacementPose()
    // Get the current screen center position and perform a raycast to check for planes
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementIndicator()
    // Update the placement indicator based on the placement pose
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceText()
    // Place the text prefab at the placement pose
    {
        if (spawnedText = null)
        {
            spawnedText = Instantiate(textPrefab, placementPose.position, placementPose.rotation);
        }
        else
        {
            spawnedText.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }

    private void PlaceObject()
    // Place the object prefab at the placement pose
    {
        if (spawnedText != null & Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(objectPrefab, placementPose.position, placementPose.rotation);
            Destroy(spawnedText);
        }
    }




}
