using System;
using UnityEngine;
using UnityEngine.Advertisements;

public enum GameState
{
    Play,
    Pause,
    Restart,
    Death,
    WatchAD,
    MainMenu,
}
public class GamePlayManager : MonoBehaviour
{
    private Gravity _gravity;
    private GameState _gameState;
    private KillType _lastDeathKillType;

    public static GamePlayManager Instance;

    private void Awake()
    {
        Instance = this;
        _gravity = GetComponent<Gravity>();
        DeathDispatcher.Instance.AddListener(WinLoseGameDefinition);
        GameStateChanger(GameState.Play);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4804595", false); //Android ID
        }
    }
    

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && _gameState == GameState.Play)
        {
            _gravity.ChangeGravity();
        }
    }

    private void OnDestroy()
    {
        DeathDispatcher.Instance.RemoveListener(WinLoseGameDefinition);
    }

    public void GameStateChanger(GameState gameState)
    {
        this._gameState = gameState;
        
        switch (gameState)
        {
            case GameState.Play:
                GameOnResume();
                break;
            case GameState.Pause:
                PlayerData.Instance.SaveBestScore();
                GameOnPause();
                break;
            case GameState.Restart:
                _gravity.RestartGravity();
                GameOnResume();
                WatchADReset();
                GameStateDispatcher.Instance.ActionWasLoaded(GameState.Restart);
                GameStateChanger(GameState.Play);
                break;
            case GameState.Death:
                GameOnPause();
                PlayerData.Instance.SaveBestScore();
                break;
            case GameState.WatchAD:
                if (_lastDeathKillType == KillType.Out) 
                                        _gravity.ChangeGravity();
                WatchAD();
                GameStateDispatcher.Instance.ActionWasLoaded(GameState.WatchAD);
                GameStateChanger(GameState.Pause);
                break;
            case GameState.MainMenu:
                WatchADReset();
                GameOnResume();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
        
        GameStateDispatcher.Instance.ActionWasLoaded(this._gameState);
    }

    private void WatchAD()
    {
        ADWatched();
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    private void WinLoseGameDefinition(KillType killType)
    {
        if (killType == KillType.Out || killType == KillType.Wall || killType == KillType.Enemy)
        { 
            _lastDeathKillType = killType;
            GameStateChanger(GameState.Death);
           
        }
        
    }
    
    private void GameOnPause()
    {
        Time.timeScale = 0f;
    }

    private void GameOnResume()
    {
        Time.timeScale = 1f;
    }

    private void WatchADReset()
    {
        PlayerData.AdAwailable = true;
    }

    private void ADWatched()
    {
        PlayerData.AdAwailable = false;
    }
}
