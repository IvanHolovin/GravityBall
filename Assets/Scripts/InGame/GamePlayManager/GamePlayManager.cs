using System;
using System.Collections;
using System.Collections.Generic;
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
    private Gravity gr;
    private GameState gameState;
    private KillType lastDeathKillType;

    public static GamePlayManager Instance;

    private void Awake()
    {
        Instance = this;
        gr = GetComponent<Gravity>();
        DeathDispatcher.Instance.AddListener(WinLoseGameDefinition);
        GameStateChanger(GameState.Play);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4804595", false); //Android ID
        }
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameState == GameState.Play)
        {
            gr.ChangeGravity();
        }
    }

    private void OnDestroy()
    {
        DeathDispatcher.Instance.RemoveListener(WinLoseGameDefinition);
    }

    public void GameStateChanger(GameState gameState)
    {
        this.gameState = gameState;
        
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
                gr.RestartGravity();
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
                if (lastDeathKillType == KillType.Out) 
                                        gr.ChangeGravity();
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
        
        GameStateDispatcher.Instance.ActionWasLoaded(this.gameState);
    }

    private void WatchAD()
    {
        ADWatched();
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
            Debug.Log("ADWATCHED");
        }
    }

    private void WinLoseGameDefinition(KillType killType)
    {
        if (killType == KillType.Out || killType == KillType.Wall || killType == KillType.Enemy)
        { 
            lastDeathKillType = killType;
            GameStateChanger(GameState.Death);
           
        }
        
    }

    private void OnRestart()
    {
        GameStateChanger(GameState.Play);
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
        PlayerData.ADAwailable = true;
    }

    private void ADWatched()
    {
        PlayerData.ADAwailable = false;
    }
    
    
}
