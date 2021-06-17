using UnityEngine;
using DG.Tweening;

public class JumpMover : MoverBase
{
    Tween jumpTween;

    public override void Move(Vector3 newPosition, Vector3 direction)
    {
        var differenceInY = GetComponent<MovableObject>().height;
        newPosition = new Vector3(newPosition.x, newPosition.y - differenceInY, newPosition.z);
        jumpTween = transform.DOJump(newPosition, 0.5f, 1, 0.2f, false).SetAutoKill(false).OnComplete(() =>
        {
            MovementCompleted(true);
        });
    }

    public override void UnMove()
    {
        jumpTween.PlayBackwards();
        jumpTween.OnRewind(() =>
        {
            MovementCompleted(false);
        });
    }
}
