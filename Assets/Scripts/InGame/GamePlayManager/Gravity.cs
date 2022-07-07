using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static event Action GravityStateChange;
    private static bool _gravityState = true;
    public static bool GravityState
    {
        get { return _gravityState; }
    }

    private void Awake()
    {
        Physics.gravity = new Vector3(0f,-40f,0f);
    }

    public void ChangeGravity()
    {
        Physics.gravity *= -1;
        _gravityState = !_gravityState;
        GravityStateChange?.Invoke();
    }

    public void RestartGravity()
    {
        if (Physics.gravity.y > 0)
        {
            Physics.gravity *= -1;
            _gravityState = !_gravityState;
            GravityStateChange?.Invoke();
        }
        
    }
}
