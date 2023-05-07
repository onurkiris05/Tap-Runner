using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public float Speed = 2;
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
            transform.Translate(0, 0, Speed * Time.deltaTime);

            yield return null; // Wait for the next frame
        }
    }
}