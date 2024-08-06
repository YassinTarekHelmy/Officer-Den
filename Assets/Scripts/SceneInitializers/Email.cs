using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Email : MonoBehaviour
{
    [SerializeField] private Button _replyButton;
    [SerializeField] private TMP_Text _sender;
    [SerializeField] private TMP_Text _urgency;

    [Header("Settings")]

    [SerializeField] private float _increasingFillAmount;

    private BoredumBar _boredomBar;
    private CameraSwitcherUI _cameraSwitcherUI;
    private TheOfficerMinigame _miniGame;
    public bool IsUrgent { get; private set; }
    public bool IsRepliedTo { get; private set; } = false;

    private void Start()
    {
        _replyButton.onClick.AddListener(Reply);
        IsUrgent = UnityEngine.Random.Range(0, 3) != 1;

        if (IsUrgent) {
            _urgency.text = "Urgent";
        } else {
            _urgency.text = "";
        }

        _cameraSwitcherUI = FindObjectOfType<CameraSwitcherUI>();
        _miniGame = ReferenceHolder.Instance.OfficerMinigame;
        _boredomBar = ReferenceHolder.Instance.BoredomBar;
    }

    private void Reply()
    {
        _miniGame.gameObject.SetActive(true);
        _cameraSwitcherUI.DisableUI();

        Action winAction = null;
        winAction = () => {
            OnWin();

            _miniGame.OnWin -= winAction;  
        };

        _miniGame.OnWin += winAction;
        
    }

    private void OnWin()
    {
        IsRepliedTo = true;
        _cameraSwitcherUI.EnableUI();
        _miniGame.gameObject.SetActive(false);

        if (!IsUrgent) {
            _boredomBar.IncreaseFill(_increasingFillAmount);
        }        
    }

    private void OnDestroy()
    {
        _replyButton.onClick.RemoveListener(Reply);
    }

    public void SetSender(string sender)
    {
        _sender.text = sender;
    }

}
