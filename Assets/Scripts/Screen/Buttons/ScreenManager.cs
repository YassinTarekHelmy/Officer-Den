using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ScreenManager : MonoBehaviour
{
    public enum ScreenState {
        None,
        Email,
        Internet,
        Work
    }
    [Header("Buttons")]
    [SerializeField] private Button _powerButton;
    [SerializeField] private Button _EmailButton;
    [SerializeField] private Button _internetButton;
    [SerializeField] private Button _workButton;

    [Header("Screens")]
    [SerializeField] private GameObject _EmailScreen;
    [SerializeField] private GameObject _internetScreen;
    [SerializeField] private GameObject _workScreen;

    [Header("Application Screens")]
    [SerializeField] private GameObject _workApplication;
    [SerializeField] private GameObject _InternetApplication;
    [SerializeField] private GameObject _EmailApplication;

    [Header("References")]
    [SerializeField] private RectTransform _screenRectTransform;
    [SerializeField] private GameObject _screenCanvas;
    [SerializeField] private BoredumBar _boredomBar; 
    
    public ScreenState _currentScreenState{ get; private set; }
    private void OnEnable() {
        _powerButton.onClick.AddListener(OnPowerButtonClick);
        _EmailButton.onClick.AddListener(OnEmailButtonClick);
        _internetButton.onClick.AddListener(OnInternetButtonClick);
        _workButton.onClick.AddListener(OnWorkButtonClick);

    }

    private void OnDisable() {
        _powerButton.onClick.RemoveListener(OnPowerButtonClick);
        _EmailButton.onClick.RemoveListener(OnEmailButtonClick);
        _internetButton.onClick.RemoveListener(OnInternetButtonClick);
        _workButton.onClick.RemoveListener(OnWorkButtonClick);
    }


    private void OnPowerButtonClick() {
        _screenCanvas.SetActive(false);
    }
    
    private void OnWorkButtonClick()
    {
        _workScreen.SetActive(true);
    }

    private void OnEmailButtonClick() {
        _EmailScreen.SetActive(true);
    }

    private void OnInternetButtonClick() {
        _internetScreen.SetActive(true);
    }

    private void Update() {
        MouseHandler();

        HandleScreenStates();
    }

    private void MouseHandler() {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_screenRectTransform, mousePosition, null, out Vector2 localPoint);
        
        localPoint.x = Mathf.Clamp(localPoint.x, _screenRectTransform.rect.xMin, _screenRectTransform.rect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, _screenRectTransform.rect.yMin, _screenRectTransform.rect.yMax);

        Vector2 clampedScreenPoint = RectTransformUtility.WorldToScreenPoint(null, _screenRectTransform.TransformPoint(localPoint));
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Cursor.SetCursor(null, clampedScreenPoint, CursorMode.Auto);
    }

    private void HandleScreenStates() {
        if (!GameManager.Instance.HasGameStarted)
            return;
            
        if (_EmailApplication.activeSelf)
        {
            _currentScreenState = ScreenState.Email;
        }
        else if (_InternetApplication.activeSelf)
        {
            _currentScreenState = ScreenState.Internet;
            _boredomBar.SetFillState(BoredumBar.FillState.Filling);
        }
        else if (_workApplication.activeSelf)
        {
            _currentScreenState = ScreenState.Work;
            _boredomBar.SetFillState(BoredumBar.FillState.Decaying);
        }
        else
        {
            _currentScreenState = ScreenState.None;
            _boredomBar.SetFillState(BoredumBar.FillState.Decaying);
        }
    }
}
