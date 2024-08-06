using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions _playerInputActions;

    public event Action OnSpacePressed = delegate {};
    public event Action ScreenCaptureEvent = delegate {};
    private void Awake() {
        if (Instance != this && Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _playerInputActions = new();
    }

    private void OnEnable() {
        _playerInputActions.Enable();
        _playerInputActions.Player.TheOfficerMiniGame.performed += TheOfficerMiniGamePerformed;
    
        _playerInputActions.Screen.ScreenCapture.performed += CaptureScreenPerformed;
    }

    private void CaptureScreenPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        ScreenCaptureEvent?.Invoke();
    }


    private void OnDisable() {
        _playerInputActions.Player.TheOfficerMiniGame.performed -= TheOfficerMiniGamePerformed;
        
        _playerInputActions.Screen.ScreenCapture.performed -= CaptureScreenPerformed;
        _playerInputActions.Disable();
    }
    
    private void TheOfficerMiniGamePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSpacePressed?.Invoke();
    }
}
