using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SuccessiveManager : MonoBehaviour
{
    [SerializeField] private AudioClip _sliceSFX;
    [SerializeField] private float _pitchIncreaseValue = 0.1f;
    [SerializeField] private float _maxPitchValue = 2f;

    AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        GameManager.Instance.OnPerfectTap += PlayPerfectSound;
        GameManager.Instance.OnSliced += PlayNormalSound;
        GameManager.Instance.OnGameOver += ResetPitch;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPerfectTap -= PlayPerfectSound;
        GameManager.Instance.OnSliced -= PlayNormalSound;
        GameManager.Instance.OnGameOver -= ResetPitch;
    }

    private void PlayPerfectSound()
    {
        _audioSource.pitch += _pitchIncreaseValue;

        if (_audioSource.pitch > _maxPitchValue)
        {
            _audioSource.pitch = _maxPitchValue;
        }

        _audioSource.Play();
    }

    private void PlayNormalSound()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(_sliceSFX);
    }

    private void ResetPitch()
    {
        _audioSource.pitch = 1f;
    }
}