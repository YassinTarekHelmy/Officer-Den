using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _officersVisuals;
    private GameObject _currentOfficerVisual;

    void Awake()
    {
        foreach (GameObject officerVisual in _officersVisuals)
        {
            officerVisual.SetActive(false);
        }        
    }

    private void OnEnable() {
        RandomizeOfficer();
    }
    private void RandomizeOfficer()
    {
        if (_currentOfficerVisual != null)
        {
            _currentOfficerVisual.SetActive(false);
        }

        int randomIndex = Random.Range(0, _officersVisuals.Count);
        _officersVisuals[randomIndex].SetActive(true);

        _currentOfficerVisual = _officersVisuals[randomIndex];
    }

}
