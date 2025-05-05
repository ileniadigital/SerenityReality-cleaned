// Handles selecting the object in the Visualisation scene before placing it
//
// It must be attached to the object manager in the AR world
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class objectManager : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array of object prefabs to select from
    public GameObject selectionMenu; // The menu to select the object
    private GameObject selectedPrefab; // The currently selected prefab
    private GameObject spawnedObject; // The currently spawned object
    private ARRaycastManager raycastManager; // The AR Raycast Manager to detect planes

    void Start()
    {
        selectionMenu.SetActive(true); // Show the menu at start

        // Automatically find the ARRaycastManager inside the scene
        raycastManager = FindObjectOfType<ARRaycastManager>();

        if (raycastManager == null)
        {
            Debug.LogError("ARRaycastManager not found! Make sure it's inside XR Origin.");
        }
    }

    public void SelectObject(int index)
    // Select object and assign it to the selectedPrefab variable
    {
        selectedPrefab = objectPrefabs[index];
        //selectionMenu.SetActive(false); // Hide menu after selection
    }

    void Update()
    // Update is called once per frame
    // Check for touch input and place the selected object in the AR world
    {
        if (selectedPrefab == null || raycastManager == null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Remove previous object before placing a new one
                if (spawnedObject != null)
                    Destroy(spawnedObject);

                spawnedObject = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
