// NOT IMPLEMENTED for the current version
//  Starfish asset was designed to be the back button. Not included
using UnityEngine;
using UnityEngine.EventSystems;

public class StarfishExit : MonoBehaviour
{
    public GameObject confirmationPanel;

    void Update()
    {
        // Make sure the user is touching the screen and not clicking on UI
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            // Ignore if touch is on UI
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log("Ray hit: " + hit.transform.name);

                if (hit.transform.gameObject == this.gameObject)
                {
                    Debug.Log("Starfish tapped!");
                    confirmationPanel.SetActive(true);
                }
            }
        }
    }
}
