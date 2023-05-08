using UnityEngine;

public class SuccessiveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioClip _sliceSFX;
    [SerializeField] private float _pitchIncreaseValue = 0.1f;
    [SerializeField] private float _maxPitchValue = 2f;

    private AudioSource _audioSource;

    #region UNITY EVENTS

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        GameManager.Instance.OnPerfectTap += PlayPerfectSound;
        GameManager.Instance.OnSliced += PlayNormalSound;
        GameManager.Instance.OnGameOver += ResetPitch;
        GameManager.Instance.OnGameWin += ResetPitch;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPerfectTap -= PlayPerfectSound;
        GameManager.Instance.OnSliced -= PlayNormalSound;
        GameManager.Instance.OnGameOver -= ResetPitch;
        GameManager.Instance.OnGameWin -= ResetPitch;
    }

    #endregion

    #region PRIVATE METHODS

    private void PlayPerfectSound()
    {
        _audioSource.pitch += _pitchIncreaseValue;

        if (_audioSource.pitch > _maxPitchValue)
            _audioSource.pitch = _maxPitchValue;

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

    #endregion
}