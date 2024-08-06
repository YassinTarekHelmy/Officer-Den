using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    private void Initialize() {
        SceneManager.LoadScene("MainMenu"); //TODO: Change this to the first scene of your game
        ScreenTransition.Instance.FadeIn(null);
    }
}
