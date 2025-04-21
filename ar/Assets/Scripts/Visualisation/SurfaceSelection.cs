using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class SurfaceSelection : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject placementIndicator;

    public GameObject textPrefab;
    public GameObject objectPrefab;
    private GameObject spawnedText;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            PlaceText();
            PlaceObject();
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceText()
    {
        if (spawnedText = null)
        {
            spawnedText = Instantiate(textPrefab, placementPose.position, placementPose.rotation);
        }
        else {
            spawnedText.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }

    private void PlaceObject()
    {
        if (spawnedText != null & Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Instantiate(objectPrefab, placementPose.position, placementPose.rotation);
            Destroy(spawnedText);
        }
    }




}
