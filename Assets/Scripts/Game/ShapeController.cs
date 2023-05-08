using DG.Tweening;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform[] _shapes;

    #region UNITY EVENTS

    private void Start()
    {
        var rotAmount = 4;
        foreach (var shape in _shapes)
        {
            shape.Rotate(Vector3.forward * rotAmount);
            rotAmount += 4;
            shape.DORotate(new Vector3(0, 0, 360), 0.5f).SetSpeedBased(true)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }
    }

    #endregion
}