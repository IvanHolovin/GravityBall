using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPopUpController : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    
    private void Awake()
    {
        resumeButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Play));
        menuButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.MainMenu));
        restartButton?.onClick.AddListener(() => GamePlayManager.Instance.GameStateChanger(GameState.Restart));
    }
}
