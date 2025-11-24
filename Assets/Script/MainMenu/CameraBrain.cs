using System;
using UnityEngine;

public class CameraBrain : MonoBehaviour
{
    public Quaternion lerpRotateTarget, lerpOriginalRotation;
    public Vector3 lerpMoveTarget, lerpOriginalPosition;
    float lerpRotationDuration, lerpRotationElapsed,
          lerpMoveDuration, lerpMoveElapsed;
    bool rotate = false, move = false;

    public delegate void DoSomething();

    public DoSomething something;
    public Action <Vector3> OnFinishedMove;
    public Action <Quaternion> OnFinishedRotate;
    public Action OnStartRotate, OnStartMove;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        if (rotate)
        {
            if (lerpRotationElapsed <= lerpRotationDuration)
            {
                
                float t = lerpRotationElapsed / lerpRotationDuration;
                transform.rotation = Quaternion.Lerp(lerpOriginalRotation, lerpRotateTarget, t);
                lerpRotationElapsed += Time.deltaTime;
            }
            else
            {
                rotate = false;
                lerpRotationElapsed = 0;
                OnFinishedRotate?.Invoke(lerpRotateTarget);
            }
        }

        if (move)
        {
            if (lerpMoveElapsed <= lerpRotationDuration)
            {
                float t = lerpMoveElapsed / lerpMoveDuration;
                transform.position = Vector3.Lerp(lerpOriginalPosition, lerpMoveTarget, t);
                lerpMoveElapsed += Time.deltaTime;
            }
            else
            {
                move = false;
                lerpMoveElapsed = 0;
                OnFinishedMove?.Invoke(lerpMoveTarget);
            }
        }
    }

    #region MoveCamera methods 
    public void MoveCamera(Vector3 newPosition)
    {
        transform.position = newPosition;
        OnStartMove?.Invoke();
    }

    public void MoveCamera(Vector3 newPosition, Transform objectToLookAt)
    {
        transform.position = newPosition;
        transform.LookAt(objectToLookAt);
        OnStartMove?.Invoke();
    }
    #endregion
    
    #region Lerpcamera methods
    public void LerpCamera(Quaternion newRotation, float rotateDuration)
    {
        lerpRotateTarget = newRotation;
        lerpOriginalRotation = transform.rotation;

        OnStartRotate?.Invoke();

        lerpRotationDuration = rotateDuration;

        rotate = true;
    }

    public void LerpCamera(Vector3 newPosition, float moveDuration)
    {
        lerpMoveTarget = newPosition;

        OnStartMove?.Invoke();
        
        lerpMoveDuration = moveDuration;

        move = true;
    }

    public void LerpCamera(Vector3 newPosition, float moveDuration,  Quaternion newRotation, float rotateDuration)
    {
        lerpRotateTarget = newRotation;
        lerpMoveTarget = newPosition;

        lerpOriginalRotation = transform.rotation;
        lerpOriginalPosition = transform.position;

        OnStartMove?.Invoke();
        OnStartRotate?.Invoke();

        lerpRotationDuration = rotateDuration;
        lerpMoveDuration = moveDuration;

        move = true;
        rotate = true;

        //Debug.Log($"LerpOriginalPosition: {lerpOriginalPosition}\nLerpOriginalRotation: {lerpOriginalRotation}");
    }
    #endregion
}
