using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlatformManager _platformManager;

    public event Action OnTap;
    public event Action OnSliced;
    public event Action OnGameOver;
    public event Action OnPerfectTap;

    private void Awake()
    {
        _platformManager.Init();
    }

    public void InvokeOnTap()
    {
        OnTap?.Invoke();
    }

    public void InvokeOnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public void InvokeOnPerfectTap()
    {
        OnPerfectTap?.Invoke();
    }

    public void InvokeOnSliced()
    {
        OnSliced?.Invoke();
    }
}