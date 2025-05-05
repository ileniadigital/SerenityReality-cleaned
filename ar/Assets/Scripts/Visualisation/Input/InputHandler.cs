// Handles the input from the user
//
// It must be attached to the InputHandler prefab in the scene
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action OnTap; // Event triggered when the user taps on the screen
    private UserInput _userInput; // Reference to the UserInput class for handling input
    private void Awake()
    // Initialise the UserInput class when the object is created
    {
        _userInput = new UserInput();
        _userInput.Enable();
    }

    private void Start()
    // Subscribe to the Tap event when the object is started
    {
        _userInput.MobileTouch.Tap.performed += OnTapPerformed;

    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    // Handle the tap event from the user input
    {
        OnTap?.Invoke();
    }

    private void OnDestroy()
    // Unsubscribe from the Tap event when the object is destroyed
    {
        _userInput.MobileTouch.Tap.performed -= OnTapPerformed;
    }
}
