using System;

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


    private Action<TValue> _onActionTrigger;

    public void AddListener(Action<TValue> listener)
    {
        _onActionTrigger += listener;
    }

    public void RemoveListener(Action<TValue> listener)
    {
        if (_onActionTrigger != null)
        {
            _onActionTrigger -= listener;
        }
    }

    public void ActionWasLoaded(TValue value)
    {
        _onActionTrigger?.Invoke(value);
    }

}
