using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlatformManager _platformManager;

    public event Action OnTap;
    
    private void Awake()
    {
        _platformManager.Init();
    }
    
    public void InvokeOnTap()
    {
        // Check if any subscribers are listening to the event
        if (OnTap != null)
        {
            // Invoke the event, triggering all the subscribed methods
            OnTap.Invoke();
        }
    }
}
