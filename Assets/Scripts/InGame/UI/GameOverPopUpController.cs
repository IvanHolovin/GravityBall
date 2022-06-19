using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUpController : MonoBehaviour
{
    [SerializeField] private Button WatchADButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    
    private void Awake()
    {
        WatchADButton?.onClick.AddListener(CheckADAwailable);
        menuButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.MainMenu));
        restartButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Restart));
    }

    private void Update()
    {
        WatchADButton.gameObject.SetActive(PlayerData.ADAwailable);
    }

    private void CheckADAwailable()
    {
        if (PlayerData.ADAwailable)
        {
            GamePlayManager.Instance.GameStateChanger(GameState.WatchAD);
        }
    }
}
