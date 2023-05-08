using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    #region UNITY EVENTS

    void Start()
    {
        GameManager.Instance.InvokeOnGameStart();
    }

    #endregion
}
