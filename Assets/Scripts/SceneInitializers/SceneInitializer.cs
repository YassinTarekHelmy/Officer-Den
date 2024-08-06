using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneInitializer : MonoBehaviour, IInitializer
{
    public abstract void Initialize();

    
    private void Start() {
        Initialize();
    }
}

