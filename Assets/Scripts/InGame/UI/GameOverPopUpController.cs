using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUpController : MonoBehaviour
{
    [SerializeField] 
    private Button _watchAdButton;
    
    [SerializeField] 
    private Button _menuButton;
    
    [SerializeField] 
    private Button _restartButton;
    
    private void Awake()
    {
        _watchAdButton?.onClick.AddListener(CheckAdAwailable);
        _menuButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.MainMenu));
        _restartButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Restart));
    }

    private void Update()
    {
        _watchAdButton.gameObject.SetActive(PlayerData.AdAwailable);
    }

    private void CheckAdAwailable()
    {
        if (PlayerData.AdAwailable)
        {
            GamePlayManager.Instance.GameStateChanger(GameState.WatchAD);
        }
    }
}
