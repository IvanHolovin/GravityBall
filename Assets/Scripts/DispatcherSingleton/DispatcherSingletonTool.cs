using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatcherSingletonTool<TClass, TValue> where TClass: DispatcherSingletonTool<TClass, TValue>, new()
{
    private static TClass _instance;

    public static TClass Instance
    {
        get { 
            if (_instance == null)
            {
                _instance = new TClass();
            }
            return _instance; }
    }


    private Action<TValue> onActionTrigger;

    public void AddListener(Action<TValue> listener)
    {
        onActionTrigger += listener;
    }

    public void RemoveListener(Action<TValue> listener)
    {
        if (onActionTrigger != null)
        {
            onActionTrigger -= listener;
        }
    }

    public void ActionWasLoaded(TValue value)
    {
        onActionTrigger?.Invoke(value);
    }

}
