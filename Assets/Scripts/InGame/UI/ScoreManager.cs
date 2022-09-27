using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _scoreSpeed;
    [SerializeField] private Text _scoreText;
    
    private float _currentScore;
    private bool _checker = true;

    public static event Action ScoreReached;

    void Awake()
    {
        GameStateDispatcher.Instance.AddListener(CheckOnPlay);
    }
    
    void Update()
    {
        if(_checker)
        {
            _currentScore += _scoreSpeed * Time.deltaTime;
            if (_currentScore < 10)
            {
                _scoreText.text = "SCORE 0000" + Mathf.RoundToInt(_currentScore); 
            }
            else if (10 < _currentScore && _currentScore < 100)
            {
                _scoreText.text = "SCORE 000" + Mathf.RoundToInt(_currentScore);
            }
            else if (100 < _currentScore && _currentScore < 1000)
            {
                _scoreText.text = "SCORE 00" + Mathf.RoundToInt(_currentScore);
            }
            else if (1000 < _currentScore && _currentScore < 10000)
            {
                _scoreText.text = "SCORE 0" + Mathf.RoundToInt(_currentScore);
            }
            else
            {
                _scoreText.text = "SCORE " + Mathf.RoundToInt(_currentScore);
            }
        }

        if (_currentScore > PlayerData.bestScore)
        {
            PlayerData.bestScore = _currentScore;
        }

        if (Mathf.Round(_currentScore) % 100 == 0)
        {
            ScoreReached?.Invoke();
        }
    }

    private void CheckOnPlay(GameState gameState)
    {
        if (gameState == GameState.Play) 
            _checker = true;
        else if (gameState == GameState.Restart)
        {
            _currentScore = 0;
            _checker = true;
        }
        else
        {
            _checker = false;
        }
    }
}
