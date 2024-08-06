using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayRegulator : MonoBehaviour
{
    [SerializeField] private List<IncidentSequence> _incidentSequence;

    [SerializeField] private EmailIncidentSequence _emailIncidentSequence;
    [SerializeField] private float _lowestTimeBetweenIncidents;
    [SerializeField] private float _highestTimeBetweenIncidents;

    [SerializeField] private CameraSwitcherUI _cameraSwitcherUI;

    [SerializeField] private TimeUI _timeUI;
    private List<IncidentSequence> availableIncidents;
    private void Awake()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        
        GameManager.Instance.OnGameStartEvent += StartDay;
        GameManager.Instance.OnGameEndEvent += EndDay;

        GameManager.Instance.SetTimeUI(_timeUI);

        availableIncidents = new List<IncidentSequence>(_incidentSequence);
        
    }

    private void EndDay(bool disableUI)
    {
        if (disableUI) {
            _cameraSwitcherUI.DisableUI();
        }

        foreach (IncidentSequence incident in _incidentSequence)
        {
            incident.gameObject.SetActive(false);
        }

        StopAllCoroutines();

    }

    private void StartDay()
    {   
        StartCoroutine(IncidentSpawner());

        if (GameManager.Instance.CurrentDay >= 3) {
            StartCoroutine(EmailIncidentSpawner());
        }
    }


    private IEnumerator IncidentSpawner()
    {
        while (true)
        {
                
            yield return new WaitForSeconds(UnityEngine.Random.Range(_lowestTimeBetweenIncidents, _highestTimeBetweenIncidents));
            
            int randomIndex = UnityEngine.Random.Range(0, _incidentSequence.Count);

            randomIndex = UnityEngine.Random.Range(0, availableIncidents.Count);

            yield return new WaitUntil(() => availableIncidents.Count > 0);
            
            List<IncidentPointData> availableInstantiationPoints = availableIncidents[randomIndex].GetAvailableInstantiationPoints();
            
            int randomInstantiationPointIndex = UnityEngine.Random.Range(0, availableInstantiationPoints.Count);

            yield return new WaitUntil(() => CameraSwitcher.Instance.currentCameraState != availableInstantiationPoints[randomInstantiationPointIndex].cameraState);

            availableIncidents[randomIndex].gameObject.SetActive(true);
            

            availableIncidents[randomIndex].transform.SetPositionAndRotation(
                availableInstantiationPoints[randomInstantiationPointIndex].incidentPoint.position,
                availableInstantiationPoints[randomInstantiationPointIndex].incidentPoint.rotation
            );
            
            availableIncidents[randomIndex].PlaySequence(availableIncidents, availableInstantiationPoints[randomInstantiationPointIndex]);
            
            availableIncidents.Remove(availableIncidents[randomIndex]);
            
        }        
    }

    private IEnumerator EmailIncidentSpawner() {
        while(true) {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_lowestTimeBetweenIncidents, _highestTimeBetweenIncidents));
            
            _emailIncidentSequence.PlaySequence();

        }
    }
    private void OnDestroy() {
        GameManager.Instance.OnGameStartEvent -= StartDay;
        GameManager.Instance.OnGameEndEvent -= EndDay;
    }
}
