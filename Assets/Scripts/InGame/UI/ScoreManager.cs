using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scoreSpeed;
    [SerializeField] private Text scoreText;
    private float currentScore;
    private bool checker = true;

    public static event Action scoreReached;

    void Awake()
    {
        GameStateDispatcher.Instance.AddListener(CheckOnPlay);
    }

    // Update is called once per frame
    void Update()
    {
        if(checker)
        {
            currentScore += scoreSpeed * Time.deltaTime;
            if (currentScore < 10)
            {
                scoreText.text = "SCORE 0000" + Mathf.RoundToInt(currentScore); 
            }
            else if (10 < currentScore && currentScore < 100)
            {
                scoreText.text = "SCORE 000" + Mathf.RoundToInt(currentScore);
            }
            else if (100 < currentScore && currentScore < 1000)
            {
                scoreText.text = "SCORE 00" + Mathf.RoundToInt(currentScore);
            }
            else if (1000 < currentScore && currentScore < 10000)
            {
                scoreText.text = "SCORE 0" + Mathf.RoundToInt(currentScore);
            }
            else
            {
                scoreText.text = "SCORE " + Mathf.RoundToInt(currentScore);
            }
        }

        if (currentScore > PlayerData.bestScore)
        {
            PlayerData.bestScore = currentScore;
        }

        if (Mathf.Round(currentScore) % 100 == 0)
        {
            scoreReached?.Invoke();
        }
    }

    private void CheckOnPlay(GameState gameState)
    {
        if (gameState == GameState.Play) 
            checker = true;
        else if (gameState == GameState.Restart)
        {
            currentScore = 0;
            checker = true;
        }
        else
        {
            checker = false;
        }
    }
    
}
