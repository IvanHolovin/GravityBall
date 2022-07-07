using UnityEngine;

public class StaticGameData : MonoBehaviour
{
    public static bool availableAD;
    public static float currentSpeed;
    

    private static StaticGameData instance;
    public static StaticGameData Instance
    {
        get
        {
            if (instance != null)
            {
                instance = new StaticGameData();
            }

            return instance;
        }
    }

    
}
