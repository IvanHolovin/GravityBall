using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPopUpController : MonoBehaviour
{
    [SerializeField] 
    private Button _resumeButton;
    
    [SerializeField] 
    private Button _menuButton;
    
    [SerializeField] 
    private Button _restartButton;
    
    private void Awake()
    {
        _resumeButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Play));
        _menuButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.MainMenu));
        _restartButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Restart));
    }
}
