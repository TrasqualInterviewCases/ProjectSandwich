using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationalMover : MoverBase
{
    Sequence s;

    public override void Move(Vector3 newPosition,Vector3 direction)
    {
        s = DOTween.Sequence().SetAutoKill(false);
        s.Append(transform.DOJump(newPosition, 0.5f, 1, 0.3f, false));
        s.Join(transform.DORotate(-Vector3.Cross(direction, transform.up) * 180, 0.3f));
        s.OnComplete(() =>
        {
            MovementCompleted(true);
        });
    }

    public override void UnMove()
    {
        s.PlayBackwards();
        s.OnRewind(() =>
        {
            MovementCompleted(false);
        });
    }
}
