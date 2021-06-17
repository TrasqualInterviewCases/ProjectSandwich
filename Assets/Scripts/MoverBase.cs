using System;
using UnityEngine;

public abstract class MoverBase : MonoBehaviour
{
    public Action<bool> OnMovementCompleted;
    public abstract void Move(Vector3 newPosition, Vector3 direction);
    public abstract void UnMove();
    public virtual void MovementCompleted(bool isForwardMovement)
    {
        OnMovementCompleted?.Invoke(isForwardMovement);
    }
}
