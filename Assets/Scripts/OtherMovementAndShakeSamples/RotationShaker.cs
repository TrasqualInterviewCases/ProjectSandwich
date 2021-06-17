using UnityEngine;
using DG.Tweening;

public class RotationShaker : ShakerBase
{
    public override void Shake(Vector3 direction)
    {
        transform.DOShakeRotation(0.5f, direction*10, 10, 90, true).OnComplete(() =>
        {
            ShakeCompleted();
        });
    }
}
