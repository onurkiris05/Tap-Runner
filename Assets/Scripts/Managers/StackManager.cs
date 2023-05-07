using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [Header("General Components")] [SerializeField]
    private Transform[] _spawnPos;

    [SerializeField] private Transform _spawner;
    [SerializeField] private Stack _startStack;
    [SerializeField] private Stack _stack;

    public Stack StartStack => _startStack;
    int _spawnIndex;

    public Stack SpawnStack(Stack lastStack)
    {
        //Switch index number to spawn stacks in order left and right
        _spawnIndex = _spawnIndex == 0 ? 1 : 0;

        //Instantiate moving stack
        Stack createdStack = Instantiate(_stack, _spawnPos[_spawnIndex].position,
            Quaternion.identity, transform);

        //Set stack size according to last stack size
        var t = createdStack.transform;
        t.localScale = new Vector3(lastStack.transform.localScale.x, t.localScale.y, t.localScale.z);
        t.forward = _spawnPos[_spawnIndex].forward;
        
        createdStack.StartMoving();

        //Move stack spawner forward according to instantiated stack size
        _spawner.Translate(0, 0, t.localScale.z);

        return createdStack;
    }
}