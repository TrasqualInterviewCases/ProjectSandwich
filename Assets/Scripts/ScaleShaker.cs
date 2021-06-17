using UnityEngine;
using DG.Tweening;

public class ScaleShaker : ShakerBase
{
    public override void Shake(Vector3 direction)
    {
        transform.DOShakeScale(0.5f, direction / 3f, 10, 90, true).OnComplete(() =>
        {
            ShakeCompleted();
        });
    }
}
