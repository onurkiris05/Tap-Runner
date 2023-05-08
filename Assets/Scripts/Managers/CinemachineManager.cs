using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CinemachineManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [SerializeField] private CinemachineVirtualCamera _danceCam;
    
    [Space][Header("Settings")]
    [SerializeField] private float _danceDuration = 6f;

    #region UNITY EVENTS

    private void OnEnable()
    {
        GameManager.Instance.OnGameWin += SetDanceCam;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameWin -= SetDanceCam;
    }

    #endregion
    
    #region PUBLIC METHODS

    public void SetDanceCam()
    {
        _followCam.Priority = 0;
        _danceCam.Priority = 10;

        var _orbitCam = _danceCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        DOTween.To(() => _orbitCam.m_Heading.m_Bias,
                x => _orbitCam.m_Heading.m_Bias = x, 180f, _danceDuration / 2)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                _orbitCam.m_Heading.m_Bias = -180f;
                DOTween.To(() => _orbitCam.m_Heading.m_Bias,
                        x => _orbitCam.m_Heading.m_Bias = x, 0f, _danceDuration / 2)
                    .SetEase(Ease.Linear).OnComplete(() =>
                    {
                        _followCam.Priority = 10;
                        _danceCam.Priority = 0;
                        GameManager.Instance.InvokeOnGameStart();
                    });
            });
    }

    #endregion
}