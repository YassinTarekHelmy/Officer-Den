using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private float _currentTime = 6f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _timeScale = 0.01f;

    public bool IsTimeOver { get; private set; } = false;

    private string _dayTimeState = "AM";
    void Update()
    {
        if (!GameManager.Instance.HasGameStarted || IsTimeOver) return;

        _currentTime += Time.deltaTime * _timeScale;
        _duration -= Time.deltaTime * _timeScale;

        if (_currentTime >= 13) {
            _dayTimeState = "PM";
            _currentTime = 1;
        }

        _timeText.text = Mathf.Floor(_currentTime).ToString() + _dayTimeState;

        if (_duration <= 0)
        {
            IsTimeOver = true;
        }

    }
}
