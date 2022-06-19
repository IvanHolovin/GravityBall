using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public static float bestScore;
    public static bool ADAwailable = true;
    private const string BESTSCORE = "BESTSCORE";
    private static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerData();
            }

            return instance;
        }
    }

    public void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetFloat(BESTSCORE, 0f);
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetFloat(BESTSCORE,bestScore);
    }
    
}
