using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text scoreText;


    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    
    void OnEnable()
    {
        Gravity.GravityStateChange += ChangeColor;
    }

    void OnDestroy()
    {
        Gravity.GravityStateChange -= ChangeColor;
    }
    
    void ChangeColor()
    {
        if (Gravity.GravityState)
        {
            scoreText.color = new Color32(0,0,0,255);
        }
        else
        {
            scoreText.color = new Color32(255,255,255,255);
        }
    }
}
