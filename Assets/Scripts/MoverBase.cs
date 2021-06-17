using System;
using UnityEngine;

public abstract class MoverBase : MonoBehaviour
{
    public Action OnMovementCompleted;
    public abstract void Move(Vector3 newPosition, Vector3 direction);
    public virtual void MovementCompleted()
    {
        OnMovementCompleted?.Invoke();
    }
}
