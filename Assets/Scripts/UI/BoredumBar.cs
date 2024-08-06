using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BoredumBar : MonoBehaviour
{
    public enum FillState {
        Decaying,
        Filling,
        None
    }
    [SerializeField] private Image _fillContentImage;
    [SerializeField] private float _maxFillAmount;
    [SerializeField] private float _fillSpeed;
    [SerializeField][Range(0,1)] private float _losePercentage;

    private float _currentFillAmmount;
    private FillState _currentFillState;

    private float _fillPercentage;
    public event Action OnBarDropEvent = delegate {};
    
    public float FillPercentage => _fillPercentage;
    private void Awake() {
        _currentFillState = FillState.None;  //should be changed to None and Decaying when the game starts.
        _currentFillAmmount = _maxFillAmount;

        GameManager.Instance.OnGameStartEvent += OnGameStarted;
        GameManager.Instance.OnGameEndEvent += OnGameEnded;
    }

    private void OnGameStarted()
    {
        GameManager.Instance.SetBoredomBar(this);

        StartDecay();
    }

    private void Update() {
        if (_currentFillState == FillState.None)
            return;
        
        if (_currentFillState == FillState.Decaying) {
            DecayTick(Time.deltaTime);
        } 
        else if (_currentFillState == FillState.Filling) {
            FillTick(Time.deltaTime);
        }
    }

    public void StartDecay() {
        _currentFillState = FillState.Decaying;
    }

    public void StopEffect() {
        _currentFillState = FillState.None;
    }

    public void StartFill() {
        _currentFillState = FillState.Filling;
    }

    private void FillTick(float deltaTime) {
        _currentFillAmmount += deltaTime * _fillSpeed;

        if (_currentFillAmmount >= _maxFillAmount) {
            _currentFillAmmount = _maxFillAmount;
        }

        UpdateFill();
    }

    private void OnGameEnded(bool hideUI) {
        StopEffect();
    }

    private void DecayTick(float deltaTime) {
        _currentFillAmmount -= Time.deltaTime * _fillSpeed;
        
        UpdateFill();
    }

    private void UpdateFill() {
        _fillPercentage = _currentFillAmmount/_maxFillAmount;

        if (_currentFillState == FillState.Decaying &&  _fillPercentage <= _losePercentage) {
            OnBarDropEvent?.Invoke();
            
            StopEffect();
        }

        _fillContentImage.fillAmount = _fillPercentage;
    }

    public void IncreaseFill(float increase) {
        _currentFillAmmount += increase;

        if (_currentFillAmmount >= _maxFillAmount) {
            _currentFillAmmount = _maxFillAmount;
        }

        UpdateFill();
    }

    public void SetFillState(FillState fillState) {
        _currentFillState = fillState;
    }
}
