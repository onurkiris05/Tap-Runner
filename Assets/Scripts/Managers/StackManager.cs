using UnityEngine;

public class StackManager : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private MovingStack _startStack;
    [SerializeField] private MovingStack _movingStack;
    [SerializeField] private FinishStack _finishStack;

    [Space] [Header("Settings")] 
    [SerializeField] private Material[] _materials;
    [SerializeField] private Transform[] _spawnPos;
    [SerializeField] private Transform _spawner;

    public MovingStack StartStack => _startStack;
    private int _spawnIndex;
    private int _matIndex;

    #region PUBLIC METHODS

    public MovingStack SpawnStack(MovingStack lastMovingStack)
    {
        //Switch index number to spawn stacks in order left and right
        _spawnIndex = _spawnIndex == 0 ? 1 : 0;

        //Instantiate moving stack
        MovingStack createdMovingStack = Instantiate(_movingStack, _spawnPos[_spawnIndex].position,
            Quaternion.identity, transform);

        //Set stack size according to last stack size
        var t = createdMovingStack.transform;
        t.localScale = new Vector3(lastMovingStack.transform.localScale.x, t.localScale.y, t.localScale.z);

        //Re-adjust the rotation of instantiated stack according to left or right
        if (_spawnIndex == 1)
        {
            createdMovingStack.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        //Move stack spawner forward according to instantiated stack size
        _spawner.Translate(0, 0, t.localScale.z);
        
        createdMovingStack.StartMoving();
        createdMovingStack.DissolveIn(_materials[_matIndex % _materials.Length]);
        _matIndex++;

        return createdMovingStack;
    }

    public FinishStack PlaceFinishStack(Transform lastStack, int lenght)
    {
        //Calculate finish stack position
        var pos = new Vector3(0, lastStack.position.y, (lastStack.position.z + lastStack.localScale.z / 2) +
                                                       (lenght * _movingStack.transform.localScale.z) +
                                                       _finishStack.transform.localScale.z / 2f);
        //Instantiate finish stack
        var finishStack = Instantiate(_finishStack, pos, Quaternion.identity, transform);

        return finishStack;
    }

    public void PlaceSpawner(Transform lastStack)
    {
        var pos = new Vector3(0, 0, (lastStack.position.z + lastStack.localScale.z / 2) +
                                    _movingStack.transform.localScale.z / 2f);
        _spawner.position = pos;
    }

    public void SliceStack(MovingStack lastMovingStack, MovingStack currentMovingStack, float remaining)
    {
        var tLast = lastMovingStack.transform;
        var tCurrent = currentMovingStack.transform;

        //Calculate on which side that piece will be sliced
        var direction = remaining > 0 ? 1f : -1f;

        //Calculate new size and position for current stack like it got sliced
        var newXSize = tLast.localScale.x - Mathf.Abs(remaining);
        var newXPos = tLast.position.x + (remaining / 2);

        //Calculate X scale of piece that will fall
        var fallingStackXSize = tCurrent.localScale.x - newXSize;

        //Apply calculated size and position to stack to pretend it sliced
        tCurrent.localScale = new Vector3(newXSize, tCurrent.localScale.y, tCurrent.localScale.z);
        tCurrent.position = new Vector3(newXPos, tCurrent.position.y, tCurrent.position.z);

        //Calculate edge of stack and position for falling piece
        var stackEdge = tCurrent.position.x + (newXSize / 2f * direction);
        var fallingStackXPos = stackEdge + fallingStackXSize / 2f * direction;

        //Pass calculated info for instantiate falling piece
        SpawnFallingStack(tCurrent, fallingStackXPos, fallingStackXSize);
    }

    #endregion

    #region PRIVATE METHODS

    private void SpawnFallingStack(Transform tCurrent, float fallingStackXPos, float fallingStackXSize)
    {
        //Create a cube
        var stack = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //Re-adjust size and position with passed info for falling piece
        stack.transform.localScale = new Vector3(fallingStackXSize, tCurrent.localScale.y, tCurrent.localScale.z);
        stack.transform.position = new Vector3(fallingStackXPos, tCurrent.position.y, tCurrent.position.z);

        //Add rigidbody to make it drop
        stack.AddComponent<FallingStack>();
        stack.GetComponent<FallingStack>().DissolveOut(_materials[(_matIndex - 1) % _materials.Length]);

        //Destroy stack after 2 seconds
        Destroy(stack, 2f);
    }

    #endregion
}