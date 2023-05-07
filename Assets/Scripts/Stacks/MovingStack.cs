using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStack : Stack
{
    [SerializeField] private float _speed = 2;
    
    private bool isMoving;

    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveStack());
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private IEnumerator MoveStack()
    {
        while (isMoving)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);

            yield return null;
        }
    }
}