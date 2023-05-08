using System;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager>
{
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameWin;
    public event Action OnTap;
    public event Action OnPerfectTap;
    public event Action OnSliced;

    public bool IsGameActive { get; private set; }

    #region PUBLIC METHODS

    public void InvokeOnGameStart()
    {
        IsGameActive = true;
        OnGameStart?.Invoke();
    }

    public void InvokeOnGameOver()
    {
        if (!IsGameActive){return;}
        IsGameActive = false;
        OnGameOver?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void InvokeOnTap()
    {
        OnTap?.Invoke();
    }

    public void InvokeOnPerfectTap()
    {
        OnPerfectTap?.Invoke();
    }

    public void InvokeOnSliced()
    {
        OnSliced?.Invoke();
    }

    public void InvokeOnWin()
    {
        OnGameWin?.Invoke();
    }

    #endregion
}