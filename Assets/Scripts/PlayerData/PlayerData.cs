using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    private const string BEST_SCORE = "BESTSCORE";
    private static PlayerData _instance;
    
    public static float bestScore;
    public static bool AdAwailable = true;
    public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerData();
            }

            return _instance;
        }
    }

    public void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetFloat(BEST_SCORE, 0f);
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetFloat(BEST_SCORE,bestScore);
    }
    
}
