using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleShaker : MonoBehaviour, IShaker
{
    public void Shake(Vector3 direction)
    {
        transform.DOShakeScale(0.5f, direction / 3f, 10, 90, true);
    }
}
