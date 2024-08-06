using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class IncidentPointData : MonoBehaviour
{
    public Transform incidentPoint;
    public CameraSwitcher.CameraState cameraState;

    private void Awake() {
        incidentPoint = transform;
    }
}
