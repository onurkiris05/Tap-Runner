using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private StackManager _stackManager;

    public Stack LastStack => _lastStack;
    private Stack _lastStack;
    private Stack _currentStack;

    private void OnEnable()
    {
        GameManager.Instance.OnTap += ProcessPlatform;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTap -= ProcessPlatform;
    }

    public void Init()
    {
        if (_lastStack == null)
        {
            _lastStack = _stackManager.StartStack;
        }

        if (_currentStack == null)
        {
            _currentStack = _stackManager.SpawnStack(_lastStack);
        }
    }

    public void ProcessPlatform()
    {
        _currentStack.StopMoving();


        _lastStack = _currentStack;
        _currentStack = _stackManager.SpawnStack(_lastStack);
    }
}