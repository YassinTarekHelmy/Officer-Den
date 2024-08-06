using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TheOfficerMinigame : MonoBehaviour
{
    [SerializeField] private float _rangeBoxWidth;
    [SerializeField] private Image _barBackgroundImage;
    [SerializeField] private Image _rangeBox;
    [SerializeField] private Image _rangePointer;
    
    [SerializeField] private float _pointerSpeed;
    private bool _isPointerMoving = true;

    public event Action OnWin = delegate {};
    private void OnEnable() {
        InputManager.Instance.OnSpacePressed += OnSpacePressed;
        GameManager.Instance.OnGameEndEvent += CutMiniGame;
        Reset();
    }

    private void Reset() {
        _isPointerMoving = true;

        //Create the rangeBox;
        CreateRangeBox();
        ResetPointer();
    }

    private void CutMiniGame(bool disableUI) {
        gameObject.SetActive(false);
    }

    private void OnSpacePressed()
    {
        _isPointerMoving = false;
    }

    private void CreateRangeBox() {

        _rangeBox.rectTransform.sizeDelta = new Vector2(_rangeBoxWidth , _barBackgroundImage.rectTransform.sizeDelta.y);  
        _rangeBox.rectTransform.anchoredPosition = new Vector2(UnityEngine.Random.Range(0, _barBackgroundImage.rectTransform.sizeDelta.x - _rangeBox.rectTransform.sizeDelta.x), 0);
    }

    private void ResetPointer() {
        _rangePointer.rectTransform.anchoredPosition = new Vector2(0, 0);
    }


    private void Update() {
        if (_isPointerMoving) {
            MovePointer();
        } else {
            //Check if the pointer is in the rangeBox;
            if (_rangePointer.rectTransform.anchoredPosition.x >= _rangeBox.rectTransform.anchoredPosition.x && _rangePointer.rectTransform.anchoredPosition.x <= _rangeBox.rectTransform.anchoredPosition.x + _rangeBox.rectTransform.sizeDelta.x) {
                OnWin?.Invoke();
                gameObject.SetActive(false);
            } else {
                Reset();
            }

        }
    }

    private void MovePointer() {
        _rangePointer.rectTransform.anchoredPosition += new Vector2(Time.deltaTime * _pointerSpeed, 0);
    }

    private void OnDisable() {
        InputManager.Instance.OnSpacePressed -= OnSpacePressed;
        GameManager.Instance.OnGameEndEvent -= CutMiniGame;
    }
}
