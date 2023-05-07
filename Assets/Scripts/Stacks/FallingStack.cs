using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingStack : Stack
{

    public override void SetMaterial(Material mat)
    {
        _meshRenderer.material = mat;
        //Do dissolve stuff here
    }
}
