using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelephoneSequence : MonoBehaviour, ISequence
{
    [SerializeField] private Button _skipButton;
    [Header("Audio Source")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _ringingClip;
    [SerializeField] private AudioClip _pickupClip;
    [SerializeField] private AudioClip _characterSound;

    [Header("Dialogue References")]
    [SerializeField] private TMP_Text _dialogueText;

    [Header("Dialogue Settings")]
    [SerializeField] private float _timeBeforeRinging = 5.0f;
    [SerializeField] private List<string> _dialogue;
    [SerializeField] private float _dialogueDelay = 1.0f;
    [SerializeField] private float _timeBetweenCharacters = 1.0f;

    private AudioClipHandler _audioClipHandler;

    private Coroutine _playSequenceCoroutine;
    private bool _isSkipped = false;
    private void Awake() {
        _audioClipHandler = new AudioClipHandler(_ringingClip, _audioSource);
        _skipButton.gameObject.SetActive(true);
        _skipButton.onClick.AddListener(OnSkipButtonClicked);
    }
    public void PlaySequence()
    {
        _playSequenceCoroutine = StartCoroutine(PlaySequenceCoroutine());
    }

    private IEnumerator PlaySequenceCoroutine()
    {
        yield return new WaitForSeconds(_timeBeforeRinging);

        _audioClipHandler.PlayAudioClip();

        while (_audioClipHandler.IsPlaying())
        {
            yield return 0;
        }
        
        _audioClipHandler.SetAudioClip(_pickupClip);
        _audioClipHandler.PlayAudioClip();

        while (_audioClipHandler.IsPlaying())
        {
            yield return 0;
        }

        //Dialogue Sequence.

        _audioClipHandler.SetAudioClip(_characterSound);
        
        _dialogueText.ForceMeshUpdate();
        foreach (var word in _dialogue)
        {
            int maxVisibleCharacters = 0;
            _dialogueText.maxVisibleCharacters = maxVisibleCharacters;
            _dialogueText.text = word;
            while (maxVisibleCharacters < word.Length)
            {
                _audioClipHandler.PlayAudioClip();
                maxVisibleCharacters++;
                _dialogueText.maxVisibleCharacters = maxVisibleCharacters;

                if (maxVisibleCharacters >= word.Length) {
                    yield return new WaitForSeconds(_dialogueDelay);
                    break;
                }

                yield return new WaitForSeconds(_timeBetweenCharacters);
            }
        }

        _dialogueText.text = "";
        GameManager.Instance.StartGame();
        _skipButton.gameObject.SetActive(false);
    }

    private void OnSkipButtonClicked() {
        _skipButton.gameObject.SetActive(false);
        _audioClipHandler.StopAudioClip();
        StopCoroutine(_playSequenceCoroutine);
        
        _dialogueText.text = "";
        GameManager.Instance.StartGame();
    }
}
