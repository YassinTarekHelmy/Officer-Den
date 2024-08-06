using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class NauseaEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private BoredumBar _boredomBar;
    
    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _heartBeatAudioClip;

    private ChromaticAberration _chromaticAbbreiation;

    private AudioClipHandler _audioClipHandler;
    
    private float _angle = 0f;
    private void Start()
    {   
        _audioClipHandler = new(_heartBeatAudioClip, _audioSource);

        if (_globalVolume.profile.TryGet<ChromaticAberration>(out ChromaticAberration temp)) {
            _chromaticAbbreiation = temp;
        }
        _audioClipHandler.Loop();
        _audioClipHandler.PlayAudioClip();
        _audioClipHandler.SetVolume(0f);
    }

    private void Update() {
        if (!GameManager.Instance.HasGameStarted)
            return;

        if (_angle >= 360f) {
            _angle = 0f;
        }
        _chromaticAbbreiation.intensity.value = Mathf.Abs(Mathf.Sin(_angle)) *  (1 - _boredomBar.FillPercentage);

        _angle += Time.deltaTime;

        _audioClipHandler.SetVolume(1 - _boredomBar.FillPercentage);

    }

}
