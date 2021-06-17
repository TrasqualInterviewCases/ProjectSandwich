using UnityEngine;
using DG.Tweening;

public class RotationalMover : MoverBase
{
    [SerializeField] private float duration = 0.1f;

    Sequence s;

    public override void Move(Vector3 newPosition,Vector3 direction)
    {
        s = DOTween.Sequence().SetAutoKill(false);
        s.Append(transform.DOJump(newPosition, 0.5f, 1, duration, false).SetEase(Ease.Linear));
        s.Join(transform.DORotate(-Vector3.Cross(direction, transform.up) * 180, duration).SetEase(Ease.Linear));
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
