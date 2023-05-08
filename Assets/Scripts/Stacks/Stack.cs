using DG.Tweening;
using UnityEngine;

public abstract class Stack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float _dissolveInDuration = 0.5f;
    [SerializeField] protected float _dissolveOutDuration = 2f;

    protected MeshRenderer _meshRenderer;
    protected readonly int _dissolveTime = Shader.PropertyToID("_DissolveTime");
    protected readonly int _edgeWidth = Shader.PropertyToID("_EdgeWidth");

    #region UNITY EVENTS

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    

    #endregion

    #region PUBLIC METHODS

    public virtual void DissolveIn(Material mat)
    {
        _meshRenderer.material = mat;
        _meshRenderer.material.SetFloat(_dissolveTime, 1f);
        _meshRenderer.material.SetFloat(_edgeWidth, 0.05f);
        DOTween.To(() => _meshRenderer.material.GetFloat(_dissolveTime),
                x => _meshRenderer.material.SetFloat(_dissolveTime, x), 0f, _dissolveInDuration)
            .SetId(gameObject.name).SetEase(Ease.Linear).OnComplete(() => { _meshRenderer.material.SetFloat(_edgeWidth, 0f); });
    }

    public virtual void DissolveOut(Material mat)
    {
        _meshRenderer.material = mat;
        _meshRenderer.material.SetFloat(_dissolveTime, 0f);
        _meshRenderer.material.SetFloat(_edgeWidth, 0.05f);
        DOTween.To(() => _meshRenderer.material.GetFloat(_dissolveTime),
                x => _meshRenderer.material.SetFloat(_dissolveTime, x), 1f, _dissolveOutDuration)
            .SetId(gameObject.name).SetEase(Ease.Linear);
    }

    #endregion
}