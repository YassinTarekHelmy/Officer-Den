using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TheOfficerSequence : IncidentSequence
{
    [SerializeField] private float _shortestIncidentTime;
    [SerializeField] private float _longestIncidentTime;

    [SerializeField] private CameraSwitcherUI cameraSwitcherUI;
    [SerializeField] private TheOfficerMinigame _officerMinigame;


    public override IEnumerator SequenceCoroutine(List<IncidentSequence> availableIncidents, IncidentPointData incidentPointData)
    {
        float incidentTime = UnityEngine.Random.Range(_shortestIncidentTime, _longestIncidentTime);
        
        while (incidentTime > 0)
        {
            incidentTime -= Time.deltaTime;

            if (incidentPointData.cameraState == CameraSwitcher.Instance.currentCameraState)
            {
                break;
            }
            yield return 0;
        }

        if (incidentTime <= 0)
        {
            //officer is angry and you Lost.
            Debug.Log("Lose.");
            GameManager.Instance.LossEndGame();
        } else {
            //officer starts the incident.

            cameraSwitcherUI.DisableUI();
            _officerMinigame.gameObject.SetActive(true);
            Action winAction = null;
            winAction = () => {
                StartCoroutine(OnWinCoroutine(incidentPointData, availableIncidents));
                _officerMinigame.OnWin -= winAction;  
            };

            _officerMinigame.OnWin += winAction;            
        }
    }


    IEnumerator OnWinCoroutine(IncidentPointData incidentPointData, List<IncidentSequence> availableIncidents) {
        cameraSwitcherUI.EnableUI();
        IsPlaying = false;

        availableIncidents.Add(this);


        yield return new WaitUntil(() => incidentPointData.cameraState != CameraSwitcher.Instance.currentCameraState);

        gameObject.SetActive(false);
    }
}
