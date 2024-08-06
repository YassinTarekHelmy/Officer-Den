using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameSceneInitializer : SceneInitializer
{
    [SerializeField] private TelephoneSequence _telephoneSequence;

    private void Awake() {
        GameManager.Instance.InitializeGameSceneReferences();
    }
    
    public override void Initialize()
    {
        _telephoneSequence.PlaySequence();
    }
}
