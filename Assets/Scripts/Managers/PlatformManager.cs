using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private StackManager _stackManager;
    [SerializeField] private float _perfectTapTolerance = 0.15f;

    public MovingStack LastMovingStack => _lastMovingStack;
    private MovingStack _lastMovingStack;
    private MovingStack _currentMovingStack;

    private void OnEnable()
    {
        GameManager.Instance.OnTap += ProcessPlatform;
        GameManager.Instance.OnPerfectTap += ProcessPerfectTap;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTap -= ProcessPlatform;
        GameManager.Instance.OnPerfectTap -= ProcessPerfectTap;
    }

    public void Init()
    {
        if (_lastMovingStack == null)
        {
            _lastMovingStack = _stackManager.StartStack;
        }

        if (_currentMovingStack == null)
        {
            _currentMovingStack = _stackManager.SpawnStack(_lastMovingStack);
        }
    }

    private void ProcessPlatform()
    {
        _currentMovingStack.StopMoving();
        
        //Calculate surplus piece according to last stack
        float remaining = _currentMovingStack.transform.position.x - _lastMovingStack.transform.position.x;

        //If miss the last stack then game over
        if (Mathf.Abs(remaining) >= _lastMovingStack.transform.localScale.x)
        {
            GameManager.Instance.InvokeOnGameOver();
            return;
        }

        //If perfectly tapped, process perfect scenario
        if (Mathf.Abs(remaining) < _perfectTapTolerance)
        {
            GameManager.Instance.InvokeOnPerfectTap();
        }
        else
        {
            _stackManager.SliceStack(_lastMovingStack, _currentMovingStack, remaining);
            _lastMovingStack = _currentMovingStack;
            GameManager.Instance.InvokeOnSliced();
        }

        _lastMovingStack = _currentMovingStack;
        _currentMovingStack = _stackManager.SpawnStack(_lastMovingStack);
    }

    private void ProcessPerfectTap()
    {
        var tLast = _lastMovingStack.transform;
        var tCurrent = _currentMovingStack.transform;

        tCurrent.position = new Vector3(tLast.position.x, tCurrent.position.y, tCurrent.position.z);
        tCurrent.localScale = tLast.localScale;
    }
}