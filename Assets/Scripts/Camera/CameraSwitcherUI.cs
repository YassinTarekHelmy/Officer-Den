using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcherUI : MonoBehaviour
{
    [SerializeField] private SwitchRegion _rightCamUI;
    [SerializeField] private SwitchRegion _leftCamUI;
    [SerializeField] private SwitchRegion _backCamUI;

    private bool _isDisabled = false;
    private void Awake()
    {   
        _rightCamUI.gameObject.SetActive(false);
        _leftCamUI.gameObject.SetActive(false);
        _backCamUI.gameObject.SetActive(false);
        
        GameManager.Instance.OnGameStartEvent += OnGameStarted;     
    }


    private void OnGameStarted()
    {
        _rightCamUI.gameObject.SetActive(true);
        _leftCamUI.gameObject.SetActive(true);
        _backCamUI.gameObject.SetActive(true);
    }

    private void OnEnable() {
        _rightCamUI.OnMouseEnterEvent += SwitchRightCam;
        _leftCamUI.OnMouseEnterEvent += SwitchLeftCam;
        _backCamUI.OnMouseEnterEvent += SwitchBackCam;
    }


    private void OnDisable() {
        _rightCamUI.OnMouseEnterEvent -= SwitchRightCam;
        _leftCamUI.OnMouseEnterEvent -= SwitchLeftCam;
        _backCamUI.OnMouseEnterEvent -= SwitchBackCam;
    }

    private void SwitchBackCam()
    {
        if (CameraSwitcher.Instance.currentCameraState == CameraSwitcher.CameraState.BackCamera)
        {
            CameraSwitcher.Instance.SwitchBackToMainCamera();
        } else {
            CameraSwitcher.Instance.SwitchBackCamera();
        }
    }

    private void SwitchRightCam() {
        CameraSwitcher.Instance.GetNextCamera();
    }

    private void SwitchLeftCam() {
        CameraSwitcher.Instance.GetPreviousCamera();
    }

    private void Update() {
        if (!GameManager.Instance.HasGameStarted || _isDisabled) return;
        
        //Check back.
        if (CameraSwitcher.Instance.currentCameraState == CameraSwitcher.CameraState.BackCamera) {
            _rightCamUI.gameObject.SetActive(false);
            _leftCamUI.gameObject.SetActive(false);
        } else {
            _rightCamUI.gameObject.SetActive(true);
            _leftCamUI.gameObject.SetActive(true);
        }
        
        //Check left.
        if (CameraSwitcher.Instance.currentCameraState == CameraSwitcher.CameraState.LeftBoundary) {
            _leftCamUI.gameObject.SetActive(false);
        } else {
            _leftCamUI.gameObject.SetActive(true);
        }

        //Check right.
        if (CameraSwitcher.Instance.currentCameraState == CameraSwitcher.CameraState.RightBoundary) {
            _rightCamUI.gameObject.SetActive(false);
        } else {
            _rightCamUI.gameObject.SetActive(true);
        }

    }

    public void DisableUI() {
        _isDisabled = true;
        _rightCamUI.gameObject.SetActive(false);
        _leftCamUI.gameObject.SetActive(false);
        _backCamUI.gameObject.SetActive(false);
    }

    public  void EnableUI() {
        _isDisabled = false;
        _backCamUI.gameObject.SetActive(true);
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStartEvent -= OnGameStarted;
    }
}
