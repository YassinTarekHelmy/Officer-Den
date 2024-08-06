using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipHandler
{
    private AudioClip _audioClip;
    private AudioSource _audioSource;

    private float _startTime;
    public AudioClipHandler(AudioClip audioClip, AudioSource audioSource)
    {
        _audioClip = audioClip;
        _audioSource = audioSource;
    }


    public void PlayAudioClip()
    {
        _audioSource.PlayOneShot(_audioClip);
        _startTime = Time.time;
    }

    public bool IsPlaying()
    {
        return Time.time - _startTime < _audioClip.length;
    } 

    public void StopAudioClip()
    {
        _audioSource.Stop();
    }

    public void PauseAudioClip()
    {
        _audioSource.Pause();
    }

    public void UnPauseAudioClip()
    {
        _audioSource.UnPause();
    }
    
    public void SetAudioClip(AudioClip audioClip)
    {
        _audioClip = audioClip;
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;

    }

    public void Loop() {
        _audioSource.loop = true;
    }
}
