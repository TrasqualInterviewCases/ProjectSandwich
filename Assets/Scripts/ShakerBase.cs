using System;
using UnityEngine;

public abstract class ShakerBase : MonoBehaviour
{
    public Action OnShakeCompleted; 
    public abstract void Shake(Vector3 direction);
    public void ShakeCompleted()
    {
        OnShakeCompleted?.Invoke();
    }
}
