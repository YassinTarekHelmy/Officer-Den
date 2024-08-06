using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailIncidentSequence : MonoBehaviour
{

    [SerializeField] private GameObject _EmailPrefab;
    
    [SerializeField] private List<string> _senders;

    [SerializeField] private GameObject _emailContainer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _EmailTime;

    private int _maxEmailsToSpawn = 3;
    private AudioClipHandler _audioClipHandler;

    private void Awake() {
        _audioClipHandler = new AudioClipHandler(_audioClip, _audioSource);
    }

    public void PlaySequence() {
        StartCoroutine(SequenceCoroutine());
    }

    public IEnumerator SequenceCoroutine() {
        GameObject email = Instantiate(_EmailPrefab, _emailContainer.transform.position, Quaternion.identity);
        email.transform.SetParent(_emailContainer.transform);
        email.transform.localPosition = Vector3.zero;


        email.GetComponent<Email>().SetSender(_senders[UnityEngine.Random.Range(0, _senders.Count)]);
        _audioClipHandler.PlayAudioClip();

        StartCoroutine(EmailSpawnedRountine(email));
        _maxEmailsToSpawn--;

        yield return null;
    }

    public IEnumerator EmailSpawnedRountine(GameObject email) {
        float timer = 0;
        
        Email emailComponent = email.GetComponent<Email>();

        if (emailComponent.IsUrgent) {
            while (timer <= _EmailTime) {
                timer += Time.deltaTime;

                if (emailComponent.IsRepliedTo) {
                    break;
                }

                yield return 0;
            }

            if (timer >= _EmailTime) {
                GameManager.Instance.LossEndGame();
            } else {
                _maxEmailsToSpawn++;
                Destroy(email);
            }
        } else {
            yield return new WaitUntil(() => emailComponent.IsRepliedTo);

            _maxEmailsToSpawn++;
            Destroy(email);
        }
    }   
}
