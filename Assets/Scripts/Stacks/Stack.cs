using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stack : MonoBehaviour
{
    protected MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public virtual void SetMaterial(Material mat)
    {
        _meshRenderer.material = mat;
    }

}
