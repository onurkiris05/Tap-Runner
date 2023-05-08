using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class FallingStack : Stack
{
    
    #region PUBLIC METHODS

    public void DissolveOut()
    {
        DOTween.Kill(gameObject.name);
        _meshRenderer.material.SetFloat(_edgeWidth, 0.05f);
        DOTween.To(() => _meshRenderer.material.GetFloat(_dissolveTime),
                x => _meshRenderer.material.SetFloat(_dissolveTime, x), 1f, _dissolveOutDuration)
            .SetEase(Ease.Linear);
    }

    #endregion
}
