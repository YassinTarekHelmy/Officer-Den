using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable() {
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable() {
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnContinueButtonClicked() {
        ScreenTransition.Instance.FadeOut(
            () => {
                GameManager.Instance.LoadGame();
                ScreenTransition.Instance.FadeIn(null);
            }
        );
    }
    private void OnNewGameButtonClicked() {
        GameManager.Instance.DeleteSave();
        
        ScreenTransition.Instance.FadeOut(
            () => {
                GameManager.Instance.LoadGame();
                ScreenTransition.Instance.FadeIn(null);
            }
        );
    }
    private void OnSettingsButtonClicked() {
        
    }
    private void OnCreditsButtonClicked() {
        //SceneManager.LoadScene("Credits");
    }
    private void OnQuitButtonClicked() {
        ScreenTransition.Instance.FadeOut(
            () => {
                Application.Quit();
            }
        );
    }
}
