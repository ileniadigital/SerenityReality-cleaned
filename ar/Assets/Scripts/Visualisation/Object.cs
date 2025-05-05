// Plaes the object in the AR world in the Visualisation scene
//
// It must be attached to the object prefab in the AR world
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceObject(ARTrackable trackableParent)
    // Place object in the AR world based on the plane position
    {
        transform.SetParent(trackableParent?.transform);
    }
}
