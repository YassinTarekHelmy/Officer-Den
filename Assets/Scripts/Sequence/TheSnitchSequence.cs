using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSnitchSequence : IncidentSequence
{
    [SerializeField] private float _shortestTimeBetweenStages;
    [SerializeField] private float _longestTimeBetweenStages;

    [SerializeField] private AudioClip _snitchAudioClip;
    
    [SerializeField] private Transform secondStatePosition;
    [SerializeField] private Transform thirdStatePosition;

    [SerializeField] private ScreenManager _screenManager;
    private Animator _snitchAnimator;

    private AudioClipHandler _audioClipHandler;

    private void Awake()
    {
        _snitchAnimator = GetComponent<Animator>();

        _audioClipHandler = new AudioClipHandler(_snitchAudioClip, GetComponent<AudioSource>());
    }

    public override IEnumerator SequenceCoroutine(List<IncidentSequence> availableIncidents, IncidentPointData incidentPointData)
    {

        //TODO: make it generic through using the incident Point data where it is not necessarly the Back Camera Only.
        
        _snitchAnimator.SetTrigger("FirstStage");
        
        _audioClipHandler.PlayAudioClip();

        yield return new WaitForSeconds(Random.Range(_shortestTimeBetweenStages, _longestTimeBetweenStages));

        yield return new WaitUntil(() => CameraSwitcher.Instance.currentCameraState != CameraSwitcher.CameraState.BackCamera);
        
        StartCoroutine(SecondStageCoroutine(availableIncidents));
        
    }

    public IEnumerator SecondStageCoroutine(List<IncidentSequence> availableIncidents)
    {
        _snitchAnimator.SetTrigger("SecondStage");
        
        transform.position = secondStatePosition.position;

        yield return new WaitForSeconds(Random.Range(_shortestTimeBetweenStages, _longestTimeBetweenStages));

        yield return new WaitUntil(() => CameraSwitcher.Instance.currentCameraState != CameraSwitcher.CameraState.BackCamera);
        
        StartCoroutine(ThirdStageCoroutine(availableIncidents));
    }

    public IEnumerator ThirdStageCoroutine(List<IncidentSequence> availableIncidents)
    {
        _snitchAnimator.SetTrigger("ThirdStage");

        transform.position = thirdStatePosition.position;

        yield return new WaitForSeconds(Random.Range(_shortestTimeBetweenStages, _longestTimeBetweenStages));

        yield return new WaitUntil(() => CameraSwitcher.Instance.currentCameraState != CameraSwitcher.CameraState.BackCamera);
    
        //TODO: Distraction is not necessarily looking in something other than work but i will leave it right now for testing.
        
        if (_screenManager._currentScreenState != ScreenManager.ScreenState.Work) {
            Debug.Log("Lose.");
             GameManager.Instance.LossEndGame();
        } else {
            _audioClipHandler.PlayAudioClip();
        }

        IsPlaying = false;

        availableIncidents.Add(this);     
        
        gameObject.SetActive(false);
    }
    
}
