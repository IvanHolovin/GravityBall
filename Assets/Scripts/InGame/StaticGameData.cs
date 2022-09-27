using UnityEngine;

public class StaticGameData : MonoBehaviour
{
    private static StaticGameData _instance;
    
    public static bool availableAD;
    public static float currentSpeed;
    
    public static StaticGameData Instance
    {
        get
        {
            if (_instance != null)
            {
                _instance = new StaticGameData();
            }
            return _instance;
        }
    }
}
