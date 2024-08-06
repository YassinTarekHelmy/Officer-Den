
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private PlayableDirector _endGameLossSequence;
    private PlayableDirector _endGameWinSequence;
    private PlayableDirector _boredomEndGameLossSequence;
    public int CurrentDay { get; private set; }
    public event Action OnGameStartEvent = delegate {};
    public event Action<bool> OnGameEndEvent = delegate {};
    public bool HasGameStarted { get; private set; } = false;
    public bool HasGameEnded { get; private set; } = false;
    
    private TimeUI _timeUI;
    private BoredumBar _boredomBar; 
    public void StartGame() {
        OnGameStartEvent?.Invoke();
        HasGameStarted = true;

        _boredomBar.OnBarDropEvent += BoredomLossEndGame;
    }

    private void Awake() {
        if (Instance != this && Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }    
    }

    public void LossEndGame() {
        HasGameStarted = false;

        HasGameEnded = true;
        OnGameEndEvent?.Invoke(false);

        _endGameLossSequence.Play();
    }

    private void BoredomLossEndGame() {
        HasGameStarted = false;

        HasGameEnded = true;
        OnGameEndEvent?.Invoke(true);

        _boredomEndGameLossSequence.Play();
    }
    private void Update() {
        if (_timeUI != null && _timeUI.IsTimeOver) {
            WinEndGame();
            _timeUI = null;
        }
    }

    public void LoadData() {
        CurrentDay = PlayerPrefs.GetInt("CurrentDay", 1);
    }
    public void WinEndGame() {
        HasGameStarted = false;
        
        CurrentDay++;
        SaveGame();

        HasGameEnded = true;
        OnGameEndEvent?.Invoke(true);

        _endGameWinSequence.Play();
        
        
    }

    public void LoadGame() {
        LoadData();

        LoadDay();
    }

    public void SaveGame() {
        PlayerPrefs.SetInt("CurrentDay", CurrentDay);
    }

    public void DeleteSave() {
        PlayerPrefs.DeleteAll();
    }

    public void LoadDay() {
        switch(CurrentDay) {
            case 1:
                SceneManager.LoadScene("Day1");
                break;
            case 2:
                SceneManager.LoadScene("Day2");
                break;
            case 3:
                SceneManager.LoadScene("Day3");
                break;
            case 4:
                SceneManager.LoadScene("Day4");
                break;
            case 5:
                SceneManager.LoadScene("Day5");
                break;
            case 6:
                CurrentDay = 1;
                SaveGame();
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void InitializeGameSceneReferences() {
        _endGameLossSequence = GameObject.FindWithTag("EndGameLossCutScene").GetComponent<PlayableDirector>();
        _endGameWinSequence = GameObject.FindWithTag("EndGameWinCutScene").GetComponent<PlayableDirector>();
        _boredomEndGameLossSequence = GameObject.FindWithTag("BoredomEndGameLossCutScene").GetComponent<PlayableDirector>();

        _endGameLossSequence.Stop();
        _endGameWinSequence.Stop();
        _boredomEndGameLossSequence.Stop();

        _endGameLossSequence.stopped += OnEndGameLossSequenceStopped;
        _boredomEndGameLossSequence.stopped += OnEndGameLossSequenceStopped;
        _endGameWinSequence.stopped += OnEndGameWinSequenceStopped;
    }

    private void OnEndGameWinSequenceStopped(PlayableDirector director)
    {
        if (HasGameEnded) {
            ScreenTransition.Instance.FadeOut(
                () => {
                    LoadDay();
                
                    ScreenTransition.Instance.FadeIn(null);
                }
            );
        }
    }

    private void OnEndGameLossSequenceStopped(PlayableDirector director)
    {
        if (HasGameEnded) {
            ScreenTransition.Instance.FadeOut(
                () => {
                    SceneManager.LoadScene("MainMenu");

                    ScreenTransition.Instance.FadeIn(null);
                }
            );
        }
    }

    public void SetTimeUI(TimeUI timeUI) {
        _timeUI = timeUI;
    }

    public void SetBoredomBar(BoredumBar boredomBar) {
        _boredomBar = boredomBar;
    }
}
