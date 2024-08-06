using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public static ReferenceHolder Instance { get; private set; }

    [SerializeField] private TheOfficerMinigame _officerMinigame;
    [SerializeField] private BoredumBar _boredomBar;

    public TheOfficerMinigame OfficerMinigame => _officerMinigame;
    public BoredumBar BoredomBar => _boredomBar;
    
    private void Awake() {
        if (Instance != this && Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
}
