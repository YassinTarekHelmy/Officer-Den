using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public static int currentNumber = 1;
    

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start() {
        InputManager.Instance.ScreenCaptureEvent += CaptureScreen;
    }
    public void CaptureScreen()
    {
        string fileName = $"ScreenShots/Screenshot{currentNumber}.png";

        if (!Directory.Exists("ScreenShots"))
        {
            Debug.Log("Directory not found, creating directory");

            Directory.CreateDirectory("ScreenShots");
        }

        if (File.Exists(fileName))
        {
            Debug.Log("File already exists, incrementing number");
            
            currentNumber++;
            
            CaptureScreen();
            
            return;
        }

        ScreenCapture.CaptureScreenshot(fileName);

        Debug.Log($"ScreenShot captured: {fileName}");
    }

}
