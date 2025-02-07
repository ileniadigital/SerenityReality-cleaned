using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class objectManager : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Drag & drop anxiety objects in Inspector
    public GameObject selectionMenu;   // Assign the UI Panel
    private GameObject selectedPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;

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
    {
        selectedPrefab = objectPrefabs[index];
        //selectionMenu.SetActive(false); // Hide menu after selection
    }

    void Update()
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
