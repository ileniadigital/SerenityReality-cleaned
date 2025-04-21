using UnityEngine;

public class StarfishExit: MonoBehaviour
{
    void OnMouseDown() // Detects clicks on the object
    {
        Debug.Log("Starfish Exit Button Clicked!");
        Application.Quit(); // Closes the application
    }
}
