using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private StackManager _stackManager;

    [Space] [Header("Settings")] 
    [SerializeField] private float _perfectTapTolerance = 0.15f;
    [Tooltip("Given value represents that platform will be formed by how many stacks")] 
    [SerializeField] private int _platformLenght = 40;

    public MovingStack LastMovingStack => _lastMovingStack;
    public FinishStack FinishStack => _finishStack;
    private MovingStack _lastMovingStack;
    private MovingStack _currentMovingStack;
    private FinishStack _finishStack;
    private bool _isPlatformFinished;

    #region UNITY EVENTS

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += Init;
        GameManager.Instance.OnTap += ProcessPlatform;
        GameManager.Instance.OnPerfectTap += ProcessPerfectTap;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Init;
        GameManager.Instance.OnTap -= ProcessPlatform;
        GameManager.Instance.OnPerfectTap -= ProcessPerfectTap;
    }

    #endregion

    #region PRIVATE METHODS

    private void Init()
    {
        Transform initStack;

        if (_lastMovingStack == null)
        {
            initStack = _stackManager.StartStack.transform;
        }
        else
            initStack = _finishStack.transform;

        _stackManager.PlaceSpawner(initStack);
        _lastMovingStack = _stackManager.StartStack;
        _finishStack = _stackManager.PlaceFinishStack(initStack, _platformLenght);
        _currentMovingStack = _stackManager.SpawnStack(_stackManager.StartStack);
        _isPlatformFinished = false;
    }

    private void ProcessPlatform()
    {
        if (_isPlatformFinished) { return; }

        _currentMovingStack.StopMoving();

        //Calculate surplus piece according to last stack
        float remaining = _currentMovingStack.transform.position.x - _lastMovingStack.transform.position.x;

        //If miss the last stack then game over
        if (Mathf.Abs(remaining) >= _lastMovingStack.transform.localScale.x)
        {
            _currentMovingStack.AddComponent<FallingStack>();
            _currentMovingStack.GetComponent<FallingStack>().DissolveOut();
            return;
        }

        //If perfectly tapped, process perfect scenario
        if (Mathf.Abs(remaining) < _perfectTapTolerance)
        {
            GameManager.Instance.InvokeOnPerfectTap();
        }
        // Otherwise process slice stack
        else
        {
            _stackManager.SliceStack(_lastMovingStack, _currentMovingStack, remaining);
            _lastMovingStack = _currentMovingStack;
            GameManager.Instance.InvokeOnSliced();
        }

        _lastMovingStack = _currentMovingStack;

        // Stop spawning stacks when reached to finish stack
        if ((_finishStack.transform.position.z - _lastMovingStack.transform.position.z) < 5f)
        {
            _isPlatformFinished = true;
            return;
        }

        _currentMovingStack = _stackManager.SpawnStack(_lastMovingStack);
    }

    private void ProcessPerfectTap()
    {
        var tLast = _lastMovingStack.transform;
        var tCurrent = _currentMovingStack.transform;

        tCurrent.position = new Vector3(tLast.position.x, tCurrent.position.y, tCurrent.position.z);
        tCurrent.localScale = new Vector3(tLast.localScale.x, tCurrent.localScale.y, tCurrent.localScale.z);
    }

    #endregion
}