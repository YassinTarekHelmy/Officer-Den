using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IncidentSequence : MonoBehaviour
{
    [SerializeField] private List<IncidentPointData> availableInstatntiationPoints;

    
    public bool IsPlaying { get; protected set; } = false;

    public void PlaySequence(List<IncidentSequence> availableIncidents, IncidentPointData incidentPointData)
    {
        IsPlaying = true;
        
        StartCoroutine(SequenceCoroutine(availableIncidents, incidentPointData));
    }

    public List<IncidentPointData> GetAvailableInstantiationPoints()
    {
        return availableInstatntiationPoints;
    }

    public abstract IEnumerator SequenceCoroutine(List<IncidentSequence> availableIncidents , IncidentPointData incidentPointData);
    
}
