using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private PlatformManager _platformManager;

    [Space] [Header("Settings")] 
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _rotationSpeed = 5;

    private Transform target;

    private void OnEnable()
    {
        GameManager.Instance.OnSliced += SetTarget;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSliced -= SetTarget;
    }

    private void Update()
    {
        LookAt();
        Move();
    }
    
    private void SetTarget()
    {
        target = _platformManager.LastMovingStack.transform;
    }

    private void Move()
    {
        transform.Translate(0, 0, _moveSpeed * Time.deltaTime);
    }

    private void LookAt()
    {
        Vector3 direction = Vector3.forward;

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.7f)
            {
                direction = (target.position - transform.position).normalized;
            }
            else
            {
                target = null;
            }
        }

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}