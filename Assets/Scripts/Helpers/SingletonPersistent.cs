using UnityEngine;

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    public static T Instance => _instance;
    private static T _instance;

    public virtual void Awake ()
    {
        if (_instance != null && _instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            _instance = this as T;
            DontDestroyOnLoad (gameObject);
        } 
    }
}