using UnityEngine;
using Cinemachine;
using System;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public enum CameraState {
        None,
        RightBoundary,
        LeftBoundary,
        BackCamera,
    }
    public static CameraSwitcher Instance { get; private set; }

    [SerializeField] private  CinemachineVirtualCamera[] _cameraList;
    [SerializeField] private CinemachineVirtualCamera _backCamera;

    private int _currentCameraIndex = 1;
    
    public CameraState currentCameraState;
    
    private void Awake() {
        if (Instance != this && Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        currentCameraState = CameraState.None;   
    }

    public void GetNextCamera() {
        if (_currentCameraIndex == _cameraList.Length - 1 || currentCameraState == CameraState.BackCamera) {
            return;
        }

        _cameraList[_currentCameraIndex].Priority = 0;
        _currentCameraIndex++;
        _cameraList[_currentCameraIndex].Priority = 10;
        
        StartCoroutine(CheckBoundary());
    }

    public void SwitchBackCamera() {
        _backCamera.Priority = 10;
        _cameraList[_currentCameraIndex].Priority = 0;
        
        currentCameraState = CameraState.BackCamera;

    }

    public void SwitchBackToMainCamera() {
        _backCamera.Priority = 0;
        _cameraList[_currentCameraIndex].Priority = 10;

        StartCoroutine(CheckBoundary());
    }

    public void GetPreviousCamera() {
        if (_currentCameraIndex == 0 || currentCameraState == CameraState.BackCamera) {
            return;
        }

        _cameraList[_currentCameraIndex].Priority = 0;
        _currentCameraIndex--;
        
        _cameraList[_currentCameraIndex].Priority = 10;

        StartCoroutine(CheckBoundary());
    }


    private IEnumerator CheckBoundary() {
        yield return new WaitForSeconds(0.3f);

        if (_currentCameraIndex == 0) {
            currentCameraState = CameraState.LeftBoundary;

        } else if (_currentCameraIndex == _cameraList.Length - 1) {
            currentCameraState = CameraState.RightBoundary;
        
        } else {
            currentCameraState = CameraState.None;
        
        }
    }
}
