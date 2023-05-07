using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    private void Update()
    {
        transform.Translate(0, 0, _moveSpeed * Time.deltaTime);
    }
}