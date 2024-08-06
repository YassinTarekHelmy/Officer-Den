using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClipBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _clipBoardContent;

    private void Start() {
        _clipBoardContent.text = "Day " + GameManager.Instance.CurrentDay;
    }
}
