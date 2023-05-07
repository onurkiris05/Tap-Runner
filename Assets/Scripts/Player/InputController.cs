using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class InputController : MonoBehaviour
{
    private Finger movementFinger;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();

        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;

        EnhancedTouchSupport.Disable();
    }

    void HandleFingerDown(Finger touchedFinger)
    {
        if (movementFinger == null)
        {
            movementFinger = touchedFinger;

            GameManager.Instance.InvokeOnTap();
        }
    }

    void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == movementFinger)
        {
            movementFinger = null;
        }
    }

    void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == movementFinger)
        {
            // Do some stuff
        }
    }
}