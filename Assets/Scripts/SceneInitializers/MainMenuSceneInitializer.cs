using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class MainMenuSceneInitializer : SceneInitializer
{
    [SerializeField] private TMP_Text _continueText;

    [SerializeField] private CinemachineVirtualCamera _cam1; 
    [SerializeField] private CinemachineVirtualCamera _cam2;
    private float time = 0.1f;

    private bool IsInitialized = false;
    public override void Initialize()
    {
        GameManager.Instance.LoadData();
        _continueText.text = "Day " + GameManager.Instance.CurrentDay; 
    }

    private void LateUpdate() {

        if (time >= 0) {
            time -= Time.deltaTime;
        } else {
            if (!IsInitialized) {
                IsInitialized = true;
                SetCameraPriorities();
            }
        }
    }

    private void SetCameraPriorities() {
        _cam1.Priority = 10;
        _cam2.Priority = 0;
    }
}
