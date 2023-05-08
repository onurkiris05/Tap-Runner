using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private PlatformManager _platformManager;

    [Space] [Header("Settings")] 
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _rotationSpeed = 5;

    private AnimationController _animController;
    private Transform _target;
    private bool _isDancing;

    #region UNITY EVENTS

    private void Awake()
    {
        _animController = GetComponent<AnimationController>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSliced += SetTarget;
        GameManager.Instance.OnGameStart += ProcessGameStart;
        GameManager.Instance.OnGameWin += ProcessGameWin;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSliced -= SetTarget;
        GameManager.Instance.OnGameStart -= ProcessGameStart;
        GameManager.Instance.OnGameWin -= ProcessGameWin;
    }

    private void Update()
    {
        LookAt();
        
        if (_isDancing) { return; }
        
        Move();
    }

    #endregion

    #region PRIVATE METHODS

    private void SetTarget()
    {
        _target = _platformManager.LastMovingStack.transform;
    }

    private void Move()
    {
        transform.Translate(0, 0, _moveSpeed * Time.deltaTime);
    }

    private void LookAt()
    {
        Vector3 direction = Vector3.forward;

        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) > 0.7f)
            {
                direction = (_target.position - transform.position).normalized;
            }
            else { _target = null; }
        }

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }

    private void ProcessGameStart()
    {
        _target = null;
        _isDancing = false;
        _animController.SetDanceAnim(false);
    }

    private void ProcessGameWin()
    {
        _target = _platformManager.FinishStack.transform;
        _isDancing = true;

        var pos = new Vector3(_target.position.x, transform.position.y, _target.position.z - 1f);
        transform.DOMove(pos, 0.7f).SetEase(Ease.Linear).OnComplete(() => 
            { _animController.SetDanceAnim(true); });
    }

    #endregion
}